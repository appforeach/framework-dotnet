using AppForeach.Framework.DependencyInjection;
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
        private readonly IExceptionEventHandler exceptionEventHandler;
        private readonly IUnhandledExceptionEventHandler unhandledExceptionEventHandler;

        public OperationExecutor(IServiceLocator serviceLocator, IFrameworkHostConfiguration hostConfiguration, IHandlerInvokerMiddleware handlerInvokerMiddleware, 
            IOperationState operationState, IExceptionEventHandler exceptionEventHandler, IUnhandledExceptionEventHandler unhandledExceptionEventHandler)
        {
            this.serviceLocator = serviceLocator;
            this.hostConfiguration = hostConfiguration;
            this.handlerInvokerMiddleware = handlerInvokerMiddleware;
            this.operationState = operationState;
            this.exceptionEventHandler = exceptionEventHandler;
            this.unhandledExceptionEventHandler = unhandledExceptionEventHandler;
        }

        public async Task<OperationResult> Execute(IBag input)
        {
            try
            {
                PrepareContext(input);

                await ExecuteMiddlewares();

                return PrepareResult();
            }
            catch(Exception ex)
            {
                var exceptionHandlingResult = exceptionEventHandler.OnException(ex);

                if(exceptionHandlingResult.IsHandled)
                {
                    return exceptionHandlingResult.Result;
                }
                
                var unhandledExceptionHandlingResult = unhandledExceptionEventHandler.OnUnhandledException(ex);

                if(unhandledExceptionHandlingResult.IsHandled)
                {
                    return unhandledExceptionHandlingResult.Result;
                }

                throw;
            }
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

            return outputState.Result;
        }
    }
}
