using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppForeach.Framework
{
    public class OperationExecutor : IOperationExecutor
    {
        private readonly IServiceLocator serviceLocator;
        private readonly IFrameworkHostConfiguration hostConfiguration;
        private readonly IHandlerInvokerMiddleware handlerInvokerMiddleware;
        private readonly IOperationState operationState;
        
        public OperationExecutor(IServiceLocator serviceLocator, IFrameworkHostConfiguration hostConfiguration, IHandlerInvokerMiddleware handlerInvokerMiddleware, 
            IOperationState operationState)
        {
            this.serviceLocator = serviceLocator;
            this.hostConfiguration = hostConfiguration;
            this.handlerInvokerMiddleware = handlerInvokerMiddleware;
            this.operationState = operationState;
        }

        public async Task<OperationResult> Execute(IBag input)
        {
            PrepareContext(input);

            await ExecuteMiddlewares();

            return PrepareResult();
        }

        private void PrepareContext(IBag input)
        {
            var state = operationState.State.Get<OperationContextState>();
            
            state.Configuration = input;

            var inputConfig = input.Get<OperationExecutionInputConfiguration>();
            state.Input = inputConfig.Input;

            state.IsOperationInputSet = true;
        }

        public Task ExecuteMiddlewares()
        {
            NextOperationDelegate callBottom = () => handlerInvokerMiddleware.ExecuteAsync(null);

            List<Type> middlewares = hostConfiguration.ConfiguredMiddlewares;

            for (int i = middlewares.Count - 1; i >= 0; i--)
            {
                var middleware = (IOperationMiddleware)serviceLocator.GetService(middlewares[i]);
                var nextMiddleware = callBottom;
                callBottom = () => middleware.ExecuteAsync(nextMiddleware);
            }

            return callBottom();
        }

        public OperationResult PrepareResult()
        {
            var outputState = operationState.State.Get<OperationOutputState>();

            var result = new OperationResult();
            result.Result = outputState.Result;
            
            return result;
        }
    }
}
