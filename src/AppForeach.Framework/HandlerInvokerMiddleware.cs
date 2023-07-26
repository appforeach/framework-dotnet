using AppForeach.Framework.DependencyInjection;
using System.Threading.Tasks;

namespace AppForeach.Framework
{
    public class HandlerInvokerMiddleware : IHandlerInvokerMiddleware
    {
        private readonly IOperationContext context;
        private readonly IScopedExecutor scopedExecutor;
        private readonly IHandlerInvoker handlerInvoker;

        public HandlerInvokerMiddleware(IOperationContext context, IScopedExecutor scopedExecutor, IHandlerInvoker handlerInvoker)
        {
            this.context = context;
            this.scopedExecutor = scopedExecutor;
            this.handlerInvoker = handlerInvoker;
        }

        public async Task ExecuteAsync(NextOperationDelegate next)
        {
            object result;

            var createScopeFacet = context.Configuration.TryGet<OperationCreateScopeForExecutionFacet>();

            if (createScopeFacet?.CreateScopeForExecution ?? false)
            {
                result = await scopedExecutor.Execute((IHandlerInvoker invoker) => invoker.Invoke(context.Input));
            }
            else
            {
                result = await handlerInvoker.Invoke(context.Input);
            }

            var outputState = context.State.Get<OperationOutputState>();
            outputState.Result.Result = result;
        }
    }
}
