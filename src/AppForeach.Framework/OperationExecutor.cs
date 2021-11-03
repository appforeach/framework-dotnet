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

        public OperationExecutor(IServiceLocator serviceLocator, IFrameworkHostConfiguration hostConfiguration, IHandlerExecutorMiddleware handlerExecutorMiddleware)
        {
            this.serviceLocator = serviceLocator;
            this.hostConfiguration = hostConfiguration;
            this.handlerExecutorMiddleware = handlerExecutorMiddleware;
        }

        public async Task<OperationResult> Execute(IBag input)
        {
            var context = PrepareContext(input);

            await ExecuteMiddlewares(context);

            return PrepareResult(context);
        }

        private IOperationContext PrepareContext(IBag input)
        {
            var context = new OperationContext();
            context.Configuration = input;

            context.State = new Bag();

            var inputConfig = input.Get<OperationExecutionInputConfiguration>();
            context.Input = inputConfig.Input;

            return context;
        }

        public Task ExecuteMiddlewares(IOperationContext context)
        {
            NextOperationDelegate callBottom = ctx => handlerExecutorMiddleware.ExecuteAsync(ctx, null);

            List<Type> middlewares = hostConfiguration.ConfiguredMiddlewares;

            for (int i = middlewares.Count - 1; i >= 0; i--)
            {
                var middleware = (IOperationMiddleware)serviceLocator.GetService(middlewares[i]);
                var nextMiddleware = callBottom;
                callBottom = ctx => middleware.ExecuteAsync(ctx, nextMiddleware);
            }

            return callBottom(context);
        }

        public OperationResult PrepareResult(IOperationContext context)
        {
            var outputState = context.State.Get<OperationOutputState>();

            var result = new OperationResult();
            result.Result = outputState.Result;
            
            return result;
        }
    }
}
