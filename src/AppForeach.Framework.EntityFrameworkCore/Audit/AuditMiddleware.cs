﻿using AppForeach.Framework.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly IDbOptionsConfigurator dbOptionsConfigurator;
        private readonly IConnectionStringProvider connectionStringProvider;
        private readonly IServiceProvider serviceProvider;

        public AuditMiddleware(IOperationContext context, ILoggingCorrelationProvider loggingCorrelationProvider,
            IDbOptionsConfigurator dbOptionsConfigurator, IConnectionStringProvider connectionStringProvider,
            IServiceProvider serviceProvider)
        {
            this.context = context;
            this.loggingCorrelationProvider = loggingCorrelationProvider;
            this.dbOptionsConfigurator = dbOptionsConfigurator;
            this.connectionStringProvider = connectionStringProvider;
            this.serviceProvider = serviceProvider;
        }

        public async Task ExecuteAsync(NextOperationDelegate next)
        {
            var auditFacet = context.Configuration.TryGet<AuditEnabledFacet>();
            bool auditEnabled = auditFacet?.AuditEnabled ?? false;

            if (auditEnabled)
            {
                var optionsBuilder = new DbContextOptionsBuilder<FrameworkDbContext>();
                dbOptionsConfigurator.SetConnectionString(optionsBuilder, connectionStringProvider.ConnectionString);
                using var db = (FrameworkDbContext)ActivatorUtilities.CreateInstance(serviceProvider, typeof(FrameworkDbContext), optionsBuilder.Options);
                
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
