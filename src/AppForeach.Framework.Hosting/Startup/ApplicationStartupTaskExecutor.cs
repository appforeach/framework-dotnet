using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AppForeach.Framework.Hosting.Startup
{
    public class ApplicationStartupTaskExecutor : IApplicationStartupTaskExecutor
    {
        private readonly IEnumerable<IApplicationStartupDescriptor> startupDescriptors;
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly ILogger<ApplicationStartupTaskExecutor> logger;

        public ApplicationStartupTaskExecutor(IEnumerable<IApplicationStartupDescriptor> startupDescriptors,
            IServiceScopeFactory serviceScopeFactory, ILogger<ApplicationStartupTaskExecutor> logger)
        {
            this.startupDescriptors = startupDescriptors;
            this.serviceScopeFactory = serviceScopeFactory;
            this.logger = logger;
        }

        public async Task<ApplicationStartupExecutionResult> ExecuteApplicationStartup(CancellationToken cancellationToken)
        {
            var executionResult = new ApplicationStartupExecutionResult();

            foreach (var taskDescriptor in startupDescriptors)
            {
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var startupTask = (IApplicationStartup)scope.ServiceProvider.GetRequiredService(taskDescriptor.ImplemenationType);

                    string startupName = taskDescriptor.ImplemenationType.Name;

                    if(taskDescriptor.Options?.RunCondition?.Invoke() ?? true) 
                    { 
                        logger.LogInformation("Executing startup task {taskName}", startupName);
                        await startupTask.Run(cancellationToken);
                        logger.LogInformation("Executed startup task {taskName}", startupName);
                    }
                    else
                    {
                        logger.LogInformation("Skipped startup task {taskName} due to RunCondition", startupName);
                    }

                    if (taskDescriptor.Options?.ApplicationTerminateCondition?.Invoke() ?? false)
                    {
                        executionResult.IsApplicationTerminationRequested = true;
                        logger.LogInformation("Startup task {taskName} requested application termination", startupName);
                    }
                }
            }

            return executionResult;
        }
    }
}
