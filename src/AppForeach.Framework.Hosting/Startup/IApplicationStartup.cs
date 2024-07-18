using System.Threading;
using System.Threading.Tasks;

namespace AppForeach.Framework.Hosting.Startup
{
    public interface IApplicationStartup
    {
        Task Run(CancellationToken cancellationToken);
    }
}
