using System.Threading;
using System.Threading.Tasks;

namespace AppForeach.Framework.EntityFrameworkCore.Audit
{
    public class AuditSaveBatchHandler
        (
        IOperationContext operationContext
        )
    {
        public async Task Handle(AuditSaveBatchCommand command, CancellationToken cancellationToken)
        {
            var scopeState = operationContext.State.Get<TransactionScopeState>();

            if (!scopeState.IsTransactionInitialized)
                throw new FrameworkException("Transaction is not initialized.");

            var db = scopeState.DbContext
                ?? throw new FrameworkException("FrameworkDbContext is not available.");

            db.Audit.AddRange(command.AuditRecords);
            await db.SaveChangesAsync(cancellationToken);
        }
    }
}
