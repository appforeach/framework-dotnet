using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using AppForeach.Framework.Validation;
using Microsoft.Extensions.Options;

namespace AppForeach.Framework.EntityFrameworkCore.Audit;

public class AuditAsyncProcessingService 
    (
    IAuditAsyncQueue asyncAuditQueue,
    IOperationMediator operationMediator,
    IOptions<AuditAsyncOptions> auditAsyncOptions
    ) : IAuditAsyncProcessingService
{
    public async Task ProcessAll(CancellationToken cancellationToken = default)
    {
        while (await asyncAuditQueue.WaitToReadAsync(cancellationToken))
        {
            List<AuditEntity> auditEntities = new List<AuditEntity>();
            int count = 0;

            while (count < auditAsyncOptions.Value.WriteBatchSize && asyncAuditQueue.TryRead(out var audit))
            {
                auditEntities.Add(audit);
                count++;
            }

            if (auditEntities.Count > 0)
            {
                var command = new AuditSaveBatchCommand
                {
                    AuditRecords = auditEntities
                };

                await operationMediator.Execute(command, SaveOperationOptions, cancellationToken);
            }
        }
    }

    private static Action<IOperationBuilder> SaveOperationOptions => op => op
        .OperationCreateScopeForExecution(true)
        .AuditEnabled(false)
        .TransactionInsertFact(false)
        .TransactionIsolationLevel(IsolationLevel.ReadCommitted)
        .TransactionRetry(true)
        .TransactionRetryCount(2)
        .TransactionMaxRetryDelay(TimeSpan.FromSeconds(1))
        .HasValidator(false);
}
