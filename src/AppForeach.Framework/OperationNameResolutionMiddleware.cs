using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppForeach.Framework
{
    public class OperationNameResolutionMiddleware : IOperationMiddleware
    {
        private readonly IOperationContext operationContext;
        private readonly IHandlerMap handlerMap;
        private readonly IOperationNameResolver operationNameResolver;

        public OperationNameResolutionMiddleware(IOperationContext operationContext, IHandlerMap handlerMap, IOperationNameResolver operationNameResolver)
        {
            this.operationContext = operationContext;
            this.handlerMap = handlerMap;
            this.operationNameResolver = operationNameResolver;
        }

        public async Task ExecuteAsync(NextOperationDelegate next, CancellationToken cancellationToken)
        {
            var contextState = operationContext.State.Get<OperationContextState>();

            if (contextState.Input == null)
            {
                throw new FrameworkException("Operation input is null.");
            }

            Type inputType = contextState.Input.GetType();
            var handlerMethod = handlerMap.GetHandlerMethod(inputType);

            if (handlerMethod == null)
            {
                throw new FrameworkException($"Handler not found for input of type { inputType }.");
            }
            
            var operationNameFacet = contextState.Configuration.TryGet<OperationNameFacet>();
            var operationIsCommandFacet = contextState.Configuration.TryGet<OperationIsCommandFacet>();
            
            OperationName operationName = null;

            if (operationNameFacet == null || operationIsCommandFacet == null)
            {
                Type handlerType = handlerMethod.DeclaringType;

                operationName = operationNameResolver.ResolveName(inputType, handlerType);
            }

            contextState.OperationName = operationNameFacet?.OperationName ?? operationName.Name;
            contextState.IsCommand = operationIsCommandFacet?.IsCommand ?? operationName.IsCommand;
            contextState.IsOperationNameResolved = true;

            await next();
        }
    }
}
