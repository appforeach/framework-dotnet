using AppForeach.Framework.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppForeach.Framework
{
    public class OperationExecutor : IOperationExecutor
    {
        private readonly IFrameworkHostConfiguration hostConfiguration;
        private readonly IServiceLocator serviceLocator;
        private readonly IScopedExecutor scopedExecutor;

        public OperationExecutor(IFrameworkHostConfiguration hostConfiguration, IServiceLocator serviceLocator, IScopedExecutor scopedExecutor)
        {
            this.hostConfiguration = hostConfiguration;
            this.serviceLocator = serviceLocator;
            this.scopedExecutor = scopedExecutor;
        }

        public async Task<OperationResult> Execute(object input, Action<IOperationBuilder> options, CancellationToken cancellationToken)
        {
            var state = PrepareContext(input, options);

            var outputState = await ExecuteMiddlewares(state, cancellationToken);

            return outputState.Result;
        }

        private OperationContextState PrepareContext(object input, Action<IOperationBuilder> options)
        {
            var state = new OperationContextState();
            
            state.Input = input;
            
            var globalConfigurationBuilder = new OperationBuilder(new FacetBag());
            hostConfiguration.OperationConfiguration?.Invoke(globalConfigurationBuilder);

            var operationConfiguration = new FacetBag(globalConfigurationBuilder.Configuration);
            var operationConfigurationBuilder = new OperationBuilder(operationConfiguration);
            options?.Invoke(operationConfigurationBuilder);
            state.Configuration = operationConfigurationBuilder.Configuration;

            state.IsOperationInputSet = true;

            return state;
        }

        public Task<OperationOutputState> ExecuteMiddlewares(OperationContextState operationState, CancellationToken cancellationToken)
        {
            var createScopeFacet = operationState.Configuration.TryGet<OperationCreateScopeForExecutionFacet>();

            if (createScopeFacet?.CreateScopeForExecution ?? false)
            {
                return scopedExecutor.Execute((IMiddlewareExecutor executor) => executor.Execute(operationState, hostConfiguration.ConfiguredMiddlewares, cancellationToken), false);
            }
            else
            {
                var middlewareExecutor = (IMiddlewareExecutor)serviceLocator.GetService(typeof(IMiddlewareExecutor));
                return middlewareExecutor.Execute(operationState, hostConfiguration.ConfiguredMiddlewares, cancellationToken);
            }
        }
    }
}
