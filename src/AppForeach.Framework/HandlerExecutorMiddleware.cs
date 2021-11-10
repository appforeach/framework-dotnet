using System.Threading.Tasks;

namespace AppForeach.Framework
{
    public class HandlerExecutorMiddleware : IHandlerExecutorMiddleware
    {
        private readonly IOperationContext context;
        private readonly IHandlerExecutor handlerExecutor;

        public HandlerExecutorMiddleware(IOperationContext context, IHandlerExecutor handlerExecutor)
        {
            this.context = context;
            this.handlerExecutor = handlerExecutor;
        }

        public async Task ExecuteAsync(NextOperationDelegate next)
        {
            object result = await handlerExecutor.Execute(context.Input);

            var outputState = context.State.Get<OperationOutputState>();
            outputState.Result = result;
        }
    }
}
