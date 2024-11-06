using System.Threading;
using System.Threading.Tasks;

namespace AppForeach.Framework.Hosting.Startup
{
    public interface IApplicationStartupTaskExecutor
    {
        Task ExecuteApplicationStartup(CancellationToken cancellationToken);
    }
}
