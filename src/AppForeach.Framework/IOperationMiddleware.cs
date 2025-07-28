using System.Threading;
using System.Threading.Tasks;

namespace AppForeach.Framework
{
    public interface IOperationMiddleware
    {
        Task ExecuteAsync(NextOperationDelegate next, CancellationToken cancellationToken);
    }
}
