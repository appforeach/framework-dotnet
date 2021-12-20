using System;
using System.Threading.Tasks;

namespace AppForeach.Framework
{
    public class OperationNameResolutionMiddleware : IOperationNameResolutionMiddleware
    {
        private readonly IOperationState operationState;
        private readonly IHandlerMap handlerMap;
        private readonly IOperationNameResolver operationNameResolver;

        public OperationNameResolutionMiddleware(IOperationState operationState, IHandlerMap handlerMap, IOperationNameResolver operationNameResolver)
        {
            this.operationState = operationState;
            this.handlerMap = handlerMap;
            this.operationNameResolver = operationNameResolver;
        }

        public async Task ExecuteAsync(NextOperationDelegate next)
        {
            var contextState = operationState.State.Get<OperationContextState>();
            var operationSpec = contextState.Configuration.Get<OperationSpecificationConfiguration>();
            
            OperationName operationName = null;

            if (string.IsNullOrEmpty(operationSpec.OperationName) || !operationSpec.IsCommand.HasValue)
            {
                Type inputType = contextState.Input.GetType();
                Type handlerType = handlerMap.GetHandlerMethod(inputType).DeclaringType;

                operationName = operationNameResolver.ResolveName(inputType, handlerType);
            }

            contextState.OperationName = string.IsNullOrEmpty(operationSpec.OperationName) ?
                operationName.Name : operationSpec.OperationName;
            contextState.IsCommand = operationSpec.IsCommand ?? operationName.IsCommand;
            contextState.IsOperationNameResolved = true;

            await next();
        }
    }
}
