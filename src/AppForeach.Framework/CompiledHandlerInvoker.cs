using AppForeach.Framework.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;

namespace AppForeach.Framework
{
    public class CompiledHandlerInvoker : IHandlerInvoker
    {
        private readonly IServiceLocator serviceLocator;
        private readonly ICompiledHandlerMap handlerMap;

        public CompiledHandlerInvoker(IServiceLocator serviceLocator, ICompiledHandlerMap handlerMap)
        {
            this.serviceLocator = serviceLocator;
            this.handlerMap = handlerMap;
        }

        public async Task<object> Invoke(object operationInput, CancellationToken cancellationToken)
        {
            var operationType = operationInput.GetType();

            var handlerInfo = handlerMap.GetHandlerInfo(operationType)
                ?? throw new FrameworkException($"Handler info for {operationType} is not registered.");

            var handler = serviceLocator.GetService(handlerInfo.HandlerType)
                ?? throw new FrameworkException($"Handler {handlerInfo.HandlerType} for {operationType} cannot be resolved from service locator.");

            return await handlerInfo.HandlerFunc(handler, operationInput, cancellationToken).ConfigureAwait(false);
        }
    }
}
