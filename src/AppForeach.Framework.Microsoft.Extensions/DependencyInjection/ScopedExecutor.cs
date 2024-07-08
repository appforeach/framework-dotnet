using AppForeach.Framework.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace AppForeach.Framework.Microsoft.Extensions.DependencyInjection
{
    internal class ScopedExecutor : IScopedExecutor
    {
        private readonly IServiceProvider serviceProvider;

        public ScopedExecutor(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task<TResult> Execute<TService, TResult>(Func<TService, Task<TResult>> executeFunction, bool transferState)
        {
            using IServiceScope scope = serviceProvider.CreateScope();

            if (transferState)
            {

                var parentScopeStateProvider = serviceProvider.GetRequiredService<IOperationStateProvider>();
                var childScopeStateProvider = scope.ServiceProvider.GetRequiredService<IOperationStateProvider>();
                childScopeStateProvider.State = parentScopeStateProvider.State;
            }

            TService service = (TService)scope.ServiceProvider.GetRequiredService(typeof(TService));
            return await executeFunction(service);
        }
    }
}
