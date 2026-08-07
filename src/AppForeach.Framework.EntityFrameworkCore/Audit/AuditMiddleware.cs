using AppForeach.Framework.Logging;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AppForeach.Framework.EntityFrameworkCore.Audit
{
    public class AuditMiddleware : IOperationMiddleware
    {
        private readonly IOperationContext context;
        private readonly ILoggingCorrelationProvider loggingCorrelationProvider;
        private readonly IDbContextActivator dbContextActivator;
        private readonly IAuditAsyncQueue asyncAuditQueue;

        public AuditMiddleware(IOperationContext context, ILoggingCorrelationProvider loggingCorrelationProvider,
            IDbContextActivator dbContextActivator, IAuditAsyncQueue asyncAuditQueue)
        {
            this.context = context;
            this.loggingCorrelationProvider = loggingCorrelationProvider;
            this.dbContextActivator = dbContextActivator;
            this.asyncAuditQueue = asyncAuditQueue;
        }

        public async Task ExecuteAsync(NextOperationDelegate next, CancellationToken cancellationToken)
        {
            var auditFacet = context.Configuration.TryGet<AuditEnabledFacet>();
            bool auditEnabled = auditFacet?.AuditEnabled ?? false;

            if (auditEnabled)
            {
                var auditAsyncFacet = context.Configuration.TryGet<AuditAsyncFacet>();
                bool auditAsync= auditAsyncFacet?.AuditAsync ?? false;

                var inputAuditEntity = GetInputAuditEntity();

                using FrameworkDbContext db = auditAsync ? null :
                    dbContextActivator.Activate<FrameworkDbContext>(DbContextOperationEnlistmentStrategy.Suppress);

                if (auditAsync)
                {
                    await asyncAuditQueue.QueueAsync(inputAuditEntity);
                }
                else
                {
                    await SaveAuditEntity(db, inputAuditEntity, cancellationToken);
                }

                await next();

                var outputAuditEntity = GetOutputAuditEntity(auditAsync ? null : inputAuditEntity);
                
                if (auditAsync)
                {
                    await asyncAuditQueue.QueueAsync(outputAuditEntity);
                }
                else
                {
                    await SaveAuditEntity(db, outputAuditEntity, cancellationToken);
                }
            }
            else
            {
                await next();
            }
        }

        private AuditEntity GetInputAuditEntity()
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

            return inputAudit;
        }

        private AuditEntity GetOutputAuditEntity(AuditEntity? inputAuditEntity)
        {
            var transactionState = context.State.Get<TransactionScopeState>();
            var loggingCorrelation = loggingCorrelationProvider.CorrelationInfo;

            AuditEntity outputAudit = new AuditEntity();
            if (context.IsCommand)
            {
                outputAudit.TransactionId = transactionState.TransactionId;
            }

            outputAudit.InputAuditId = inputAuditEntity?.Id;
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

            return outputAudit;
        }

        private async Task SaveAuditEntity(FrameworkDbContext db, AuditEntity auditEntity, CancellationToken cancellationToken)
        {
            db.Audit.Add(auditEntity);
            await db.SaveChangesAsync(cancellationToken);
        }
    }
}
