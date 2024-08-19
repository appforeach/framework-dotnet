using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;

namespace AppForeach.Framework.Hosting.Startup
{
    public static class ApplicationStartupExecutorHostExtensions
    {
        public static void ExecuteApplicationStartupTasks(this IHost host)
        {
            var startupExecutor = host.Services.GetRequiredService<IApplicationStartupTaskExecutor>();
            startupExecutor.ExecuteApplicationStartup(CancellationToken.None).Wait();
        }
    }
}
