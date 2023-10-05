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

        public async Task<TResult> Execute<TService, TResult>(Func<TService, Task<TResult>> executeFunction)
        {
            using var scope = lifetimeScope.BeginLifetimeScope(builder =>
            {
                builder.Register(context => lifetimeScope.Resolve<IOperationContext>()).InstancePerLifetimeScope();
            });
            TService service = scope.Resolve<TService>();
            return await executeFunction(service);
        }
    }
}
