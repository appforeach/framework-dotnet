using AppForeach.Framework.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace AppForeach.Framework
{
    public class OperationExecutor : IOperationExecutor
    {
        private readonly IFrameworkHostConfiguration hostConfiguration;
        private readonly IMiddlewareExecutor middlewareExecutor;
        private readonly IScopedExecutor scopedExecutor;

        public OperationExecutor(IFrameworkHostConfiguration hostConfiguration, IMiddlewareExecutor middlewareExecutor, IScopedExecutor scopedExecutor)
        {
            this.hostConfiguration = hostConfiguration;
            this.middlewareExecutor = middlewareExecutor;
            this.scopedExecutor = scopedExecutor;
        }

        public async Task<OperationResult> Execute(object input, Action<IOperationBuilder> options)
        {
            var state = PrepareContext(input, options);

            var outputState = await ExecuteMiddlewares(state);

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

        public Task<OperationOutputState> ExecuteMiddlewares(OperationContextState operationState)
        {
            var createScopeFacet = operationState.Configuration.TryGet<OperationCreateScopeForExecutionFacet>();

            if (createScopeFacet?.CreateScopeForExecution ?? false)
            {
                return scopedExecutor.Execute((IMiddlewareExecutor executor) => executor.Execute(operationState, hostConfiguration.ConfiguredMiddlewares), false);
            }
            else
            {
                return middlewareExecutor.Execute(operationState, hostConfiguration.ConfiguredMiddlewares);
            }
        }
    }
}
