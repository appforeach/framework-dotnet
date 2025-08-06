using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;

namespace AppForeach.Framework.Hosting.Startup
{
    public static class ApplicationStartupExecutorHostExtensions
    {
        public static ApplicationStartupExecutionResult ExecuteApplicationStartupTasks(this IHost host)
        {
            var startupExecutor = host.Services.GetRequiredService<IApplicationStartupTaskExecutor>();
            var executionResult = startupExecutor.ExecuteApplicationStartup(CancellationToken.None).Result;
            return executionResult;
        }
    }
}
