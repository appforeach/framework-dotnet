using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppForeach.Framework
{
    public class OperationNameResolutionMiddleware : IOperationMiddleware
    {
        private readonly IOperationContext operationContext;
        private readonly IHandlerInfoProvider handlerInfoProvider;
        private readonly IOperationNameResolver operationNameResolver;

        public OperationNameResolutionMiddleware(IOperationContext operationContext, IHandlerInfoProvider handlerInfoProvider, IOperationNameResolver operationNameResolver)
        {
            this.operationContext = operationContext;
            this.handlerInfoProvider = handlerInfoProvider;
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
            var handlerType = handlerInfoProvider.GetHandlerType(inputType);

            if (handlerType == null)
            {
                throw new FrameworkException($"Handler not found for input of type { inputType }.");
            }
            
            var operationNameFacet = contextState.Configuration.TryGet<OperationNameFacet>();
            var operationIsCommandFacet = contextState.Configuration.TryGet<OperationIsCommandFacet>();
            
            OperationName operationName = null;

            if (operationNameFacet == null || operationIsCommandFacet == null)
            {
                operationName = operationNameResolver.ResolveName(inputType, handlerType);
            }

            contextState.OperationName = operationNameFacet?.OperationName ?? operationName.Name;
            contextState.IsCommand = operationIsCommandFacet?.IsCommand ?? operationName.IsCommand;
            contextState.IsOperationNameResolved = true;

            await next();
        }
    }
}
