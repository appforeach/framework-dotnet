using System;
using System.Threading.Tasks;

namespace AppForeach.Framework
{
    public class ExceptionHandlerMiddleware : IOperationMiddleware
    {
        private readonly IOperationContext context;
        private readonly IExceptionEventHandler exceptionEventHandler;
        private readonly IUnhandledExceptionEventHandler unhandledExceptionEventHandler;

        public ExceptionHandlerMiddleware(IOperationContext context, IExceptionEventHandler exceptionEventHandler, IUnhandledExceptionEventHandler unhandledExceptionEventHandler)
        {
            this.context = context;
            this.exceptionEventHandler = exceptionEventHandler;
            this.unhandledExceptionEventHandler = unhandledExceptionEventHandler;
        }

        public async Task ExecuteAsync(NextOperationDelegate next)
        {
            try
            {
                await next();
            }
            catch (Exception ex)
            {
                var outputState = context.State.Get<OperationOutputState>();

                var exceptionHandlingResult = exceptionEventHandler.OnException(ex);

                if (exceptionHandlingResult.IsHandled)
                {
                    outputState.Result = exceptionHandlingResult.Result;
                }

                var unhandledExceptionHandlingResult = unhandledExceptionEventHandler.OnUnhandledException(ex);

                if (unhandledExceptionHandlingResult.IsHandled)
                {
                    outputState.Result = unhandledExceptionHandlingResult.Result;
                }

                throw;
            }
        }
    }
}
