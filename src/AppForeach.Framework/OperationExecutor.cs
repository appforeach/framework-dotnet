using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppForeach.Framework
{
    public class OperationExecutor : IOperationExecutor
    {
        private readonly IServiceLocator serviceLocator;
        private readonly IFrameworkHostConfiguration hostConfiguration;
        private readonly IHandlerExecutorMiddleware handlerExecutorMiddleware;
        private readonly IOperationContext operationContext;

        public OperationExecutor(IServiceLocator serviceLocator, IFrameworkHostConfiguration hostConfiguration, IHandlerExecutorMiddleware handlerExecutorMiddleware, 
            IOperationContext operationContext)
        {
            this.serviceLocator = serviceLocator;
            this.hostConfiguration = hostConfiguration;
            this.handlerExecutorMiddleware = handlerExecutorMiddleware;
            this.operationContext = operationContext;
        }

        public async Task<OperationResult> Execute(IBag input)
        {
            PrepareContext(input);

            await ExecuteMiddlewares();

            return PrepareResult();
        }

        private void PrepareContext(IBag input)
        {
            operationContext.Configuration = input;

            var inputConfig = input.Get<OperationExecutionInputConfiguration>();
            operationContext.Input = inputConfig.Input;
        }

        public Task ExecuteMiddlewares()
        {
            NextOperationDelegate callBottom = () => handlerExecutorMiddleware.ExecuteAsync(null);

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
            var outputState = operationContext.State.Get<OperationOutputState>();

            var result = new OperationResult();
            result.Result = outputState.Result;
            
            return result;
        }
    }
}
