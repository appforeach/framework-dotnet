using System.Threading.Tasks;

namespace AppForeach.Framework
{
    public class HandlerExecutorMiddleware : IHandlerExecutorMiddleware
    {
        private readonly IHandlerExecutor handlerExecutor;

        public HandlerExecutorMiddleware(IHandlerExecutor handlerExecutor)
        {
            this.handlerExecutor = handlerExecutor;
        }

        public async Task ExecuteAsync(IOperationContext context, NextOperationDelegate next)
        {
            object result = await handlerExecutor.Execute(context.Input);

            var outputState = context.State.Get<OperationOutputState>();
            outputState.Result = result;
        }
    }
}
