using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
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

        public async Task ExecuteApplicationStartup(CancellationToken cancellationToken)
        {
            foreach (var taskDescriptor in startupDescriptors)
            {
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var startupTask = (IApplicationStartup)scope.ServiceProvider.GetRequiredService(taskDescriptor.ImplemenationType);

                    string startupName = taskDescriptor.GetType().Name;
                    logger.LogInformation("Executing startup task {taskName}", startupName);
                    await startupTask.Run(CancellationToken.None);
                    logger.LogInformation("Executed startup task {taskName}", startupName);
                }
            }
        }
    }
}
