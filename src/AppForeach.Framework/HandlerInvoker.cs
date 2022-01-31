using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppForeach.Framework
{
    public class HandlerInvoker : IHandlerInvoker
    {
        private readonly IServiceLocator serviceLocator;
        private readonly IHandlerMap handlerMap;

        public HandlerInvoker(IServiceLocator serviceLocator, IHandlerMap handlerMap)
        {
            this.serviceLocator = serviceLocator;
            this.handlerMap = handlerMap;
        }

        public async Task<object> Invoke(object operationInput)
        {
            Type operationType = operationInput.GetType();

            var handlerMethod = handlerMap.GetHandlerMethod(operationType);

            var handlerMethodParameters = handlerMethod.GetParameters();
            object[] invocationParameters;

            if (handlerMethodParameters.Length == 1)
            {
                invocationParameters = new object[] { operationInput };
            }
            else if(handlerMethodParameters.Length == 2 && handlerMethodParameters[1].ParameterType == typeof(CancellationToken))
            {
                invocationParameters = new object[] { operationInput, null /* TODO: get token from cancellation token provider */ };
            }
            else
            {
                throw new FrameworkException("Handler method should have request input parameter and optionally CancellationToken");
            }

            object handler = serviceLocator.GetService(handlerMethod.DeclaringType);

            Task task = (Task)handlerMethod.Invoke(handler, invocationParameters);

            await task;

            var property = task.GetType().GetProperty("Result");
            return property.GetValue(task);
        }
    }
}
