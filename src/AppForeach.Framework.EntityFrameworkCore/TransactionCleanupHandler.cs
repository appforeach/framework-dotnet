using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AppForeach.Framework.EntityFrameworkCore;

public class TransactionCleanupHandler
    (
    IOptionsSnapshot<TransactionCleanupOptions> options,
    IOperationContext context
    )
{
    public async Task Handle(TransactionCleanupCommand command, CancellationToken cancellationToken)
    {
        if (options.Value.RetentionDays <= 0)
        {
            throw new InvalidOperationException("Retention period in days must be positive.");
        }

        var scopeState = context.State.Get<TransactionScopeState>();
        var db = scopeState.DbContext;

        var from = DateTimeOffset.UtcNow.AddDays(-options.Value.RetentionDays);

        var ids = await db.Transactions
                .Where(x => x.OccuredOn < from)
                .OrderBy(x => x.OccuredOn)
                .Select(x => x.Id)
                .Take(options.Value.BatchSize)
                .ToListAsync(cancellationToken);

        await db.Transactions
                .Where(x => ids.Contains(x.Id))
                .ExecuteDeleteAsync(cancellationToken);
    }
}
