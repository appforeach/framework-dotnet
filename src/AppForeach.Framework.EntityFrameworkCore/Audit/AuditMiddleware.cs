using AppForeach.Framework.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;

namespace AppForeach.Framework.EntityFrameworkCore.Audit
{
    public class AuditMiddleware : IOperationMiddleware
    {
        private readonly IOperationContext context;
        private readonly ILoggingCorrelationProvider loggingCorrelationProvider;
        private readonly IDbContextInternalActivator dbContextInternalActivator;

        public AuditMiddleware(IOperationContext context, ILoggingCorrelationProvider loggingCorrelationProvider,
            IDbContextInternalActivator dbContextInternalActivator)
        {
            this.context = context;
            this.loggingCorrelationProvider = loggingCorrelationProvider;
            this.dbContextInternalActivator = dbContextInternalActivator;
        }

        public async Task ExecuteAsync(NextOperationDelegate next)
        {
            var auditFacet = context.Configuration.TryGet<AuditEnabledFacet>();
            bool auditEnabled = auditFacet?.AuditEnabled ?? false;

            if (auditEnabled)
            {
                using var db = dbContextInternalActivator.Activate<FrameworkDbContext>();

                var inputAudit = await AuditInput(db);

                await next();

                await AuditOutput(db, inputAudit);
            }
            else
            {
                await next();
            }
        }

        private async Task<AuditEntity> AuditInput(FrameworkDbContext db)
        {
            var transactionState = context.State.Get<TransactionScopeState>();
            var loggingCorrelation = loggingCorrelationProvider.CorrelationInfo;

            AuditEntity inputAudit = new AuditEntity();
            if (context.IsCommand)
            {
                inputAudit.TransactionId = transactionState.TransactionId;
            }

            inputAudit.OperationName = context.OperationName;
            inputAudit.IsCommand = context.IsCommand;
            inputAudit.IsInput = true;
            inputAudit.OccuredOn = DateTimeOffset.Now;
            inputAudit.LoggingTraceId = loggingCorrelation.TraceId;
            inputAudit.LoggingTransactionId = loggingCorrelation.TransactionId;
            inputAudit.Type = context.Input?.GetType()?.FullName;
            inputAudit.Payload = JsonSerializer.Serialize(context.Input);

            db.Audit.Add(inputAudit);
            await db.SaveChangesAsync();

            return inputAudit;
        }

        private async Task AuditOutput(FrameworkDbContext db, AuditEntity inputAuditEntity)
        {
            var transactionState = context.State.Get<TransactionScopeState>();
            var loggingCorrelation = loggingCorrelationProvider.CorrelationInfo;

            AuditEntity outputAudit = new AuditEntity();
            if (context.IsCommand)
            {
                outputAudit.TransactionId = transactionState.TransactionId;
            }

            outputAudit.InputAuditId = inputAuditEntity.Id;
            outputAudit.OperationName = context.OperationName;
            outputAudit.IsCommand = context.IsCommand;
            outputAudit.IsInput = false;
            outputAudit.OccuredOn = DateTimeOffset.Now;
            outputAudit.LoggingTraceId = loggingCorrelation.TraceId;
            outputAudit.LoggingTransactionId = loggingCorrelation.TransactionId;

            var outputState = context.State.Get<OperationOutputState>();

            outputAudit.Outcome = outputState?.Result?.Outcome;
            outputAudit.Type = outputState?.Result?.Result?.GetType()?.FullName;
            outputAudit.Payload = JsonSerializer.Serialize(outputState?.Result?.Result);

            db.Audit.Add(outputAudit);
            await db.SaveChangesAsync();
        }
    }
}
