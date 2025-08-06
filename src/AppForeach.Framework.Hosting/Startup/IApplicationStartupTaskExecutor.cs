using System.Threading;
using System.Threading.Tasks;

namespace AppForeach.Framework.Hosting.Startup
{
    public interface IApplicationStartupTaskExecutor
    {
        Task<ApplicationStartupExecutionResult> ExecuteApplicationStartup(CancellationToken cancellationToken);
    }
}
