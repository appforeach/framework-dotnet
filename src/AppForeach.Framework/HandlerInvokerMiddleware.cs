using System.Threading.Tasks;

namespace AppForeach.Framework
{
    public class HandlerInvokerMiddleware : IHandlerInvokerMiddleware
    {
        private readonly IOperationContext context;
        private readonly IHandlerInvoker handlerInvoker;

        public HandlerInvokerMiddleware(IOperationContext context, IHandlerInvoker handlerInvoker)
        {
            this.context = context;
            this.handlerInvoker = handlerInvoker;
        }

        public async Task ExecuteAsync(NextOperationDelegate next)
        {
            object result = await handlerInvoker.Invoke(context.Input);

            var outputState = context.State.Get<OperationOutputState>();
            outputState.Result.Result = result;
        }
    }
}
