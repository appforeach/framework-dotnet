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
        private readonly IOperationContext operationContext;
        private readonly IExceptionEventHandler exceptionEventHandler;
        private readonly IUnhandledExceptionEventHandler unhandledExceptionEventHandler;

        public OperationExecutor(IServiceLocator serviceLocator, IFrameworkHostConfiguration hostConfiguration, IHandlerInvokerMiddleware handlerInvokerMiddleware, 
            IOperationContext operationContext, IExceptionEventHandler exceptionEventHandler, IUnhandledExceptionEventHandler unhandledExceptionEventHandler)
        {
            this.serviceLocator = serviceLocator;
            this.hostConfiguration = hostConfiguration;
            this.handlerInvokerMiddleware = handlerInvokerMiddleware;
            this.operationContext = operationContext;
            this.exceptionEventHandler = exceptionEventHandler;
            this.unhandledExceptionEventHandler = unhandledExceptionEventHandler;
        }

        public async Task<OperationResult> Execute(object input, Action<IOperationBuilder> options)
        {
            try
            {
                PrepareContext(input, options);

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

        private void PrepareContext(object input, Action<IOperationBuilder> options)
        {
            var state = operationContext.State.Get<OperationContextState>();
            
            state.Input = input;
            
            var globalConfigurationBuilder = new OperationBuilder(new FacetBag());
            hostConfiguration.OperationConfiguration?.Invoke(globalConfigurationBuilder);

            var operationConfiguration = new FacetBag(globalConfigurationBuilder.Configuration);
            var operationConfigurationBuilder = new OperationBuilder(operationConfiguration);
            options?.Invoke(operationConfigurationBuilder);
            state.Configuration = operationConfigurationBuilder.Configuration;

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
            var outputState = operationContext.State.Get<OperationOutputState>();

            return outputState.Result;
        }
    }
}
