using AppForeach.Framework.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AppForeach.Framework
{
    internal class MiddlewareExecutor : IMiddlewareExecutor
    {
        private readonly IHandlerInvokerMiddleware handlerInvokerMiddleware;
        private readonly IServiceLocator serviceLocator;
        private readonly IOperationStateProvider operationStateProvider;

        public MiddlewareExecutor(IHandlerInvokerMiddleware handlerInvokerMiddleware, IServiceLocator serviceLocator,
            IOperationStateProvider operationStateProvider
            )
        {
            this.handlerInvokerMiddleware = handlerInvokerMiddleware;
            this.serviceLocator = serviceLocator;
            this.operationStateProvider = operationStateProvider;
        }

        public async Task<OperationOutputState> Execute(OperationContextState state, List<Type> middlewares, CancellationToken cancellationToken)
        {
            operationStateProvider.State.Set(state);

            await ExecuteMiddlewares(middlewares, cancellationToken);

            return operationStateProvider.State.Get<OperationOutputState>();
        }

        private Task ExecuteMiddlewares(List<Type> middlewares, CancellationToken cancellationToken)
        {
            NextOperationDelegate callBottom = () => handlerInvokerMiddleware.ExecuteAsync(null, cancellationToken);

            for (int i = middlewares.Count - 1; i >= 0; i--)
            {
                var middleware = (IOperationMiddleware)serviceLocator.GetService(middlewares[i]);
                var nextMiddleware = callBottom;
                callBottom = () => middleware.ExecuteAsync(nextMiddleware, cancellationToken);
            }

            return callBottom();
        }
    }
}
