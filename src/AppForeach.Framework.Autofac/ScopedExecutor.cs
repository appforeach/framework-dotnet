using AppForeach.Framework.DependencyInjection;
using Autofac;
using System;
using System.Threading.Tasks;

namespace AppForeach.Framework.Autofac
{
    public class ScopedExecutor : IScopedExecutor
    {
        private readonly ILifetimeScope lifetimeScope;

        public ScopedExecutor(ILifetimeScope lifetimeScope)
        {
            this.lifetimeScope = lifetimeScope;
        }

        public async Task<TResult> Execute<TService, TResult>(Func<TService, Task<TResult>> executeFunction, bool transferState)
        {
            using var childScope = lifetimeScope.BeginLifetimeScope();

            if (transferState)
            {
                var parentScopeStateProvider = lifetimeScope.Resolve<IOperationStateProvider>();
                var childScopeStateProvider = childScope.Resolve<IOperationStateProvider>();

                childScopeStateProvider.State = parentScopeStateProvider.State;
            }

            TService service = childScope.Resolve<TService>();
            return await executeFunction(service);
        }
    }
}
