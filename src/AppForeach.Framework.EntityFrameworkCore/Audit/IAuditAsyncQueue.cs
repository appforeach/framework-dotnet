using System.Threading;
using System.Threading.Tasks;

namespace AppForeach.Framework.EntityFrameworkCore.Audit
{
    public interface IAuditAsyncQueue
    {
        ValueTask QueueAsync(AuditEntity auditEntity);

        ValueTask<bool> WaitToReadAsync(CancellationToken cancellationToken = default);

        bool TryRead(out AuditEntity auditEntity);
    }
}
