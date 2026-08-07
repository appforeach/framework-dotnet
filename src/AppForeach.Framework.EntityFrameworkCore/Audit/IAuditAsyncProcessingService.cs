using System.Threading;
using System.Threading.Tasks;

namespace AppForeach.Framework.EntityFrameworkCore.Audit
{
    public interface IAuditAsyncProcessingService
    {
        Task ProcessAll(CancellationToken cancellationToken = default);
    }
}
