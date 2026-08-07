using System;

namespace AppForeach.Framework
{
    internal class HandlerInfoProvider : IHandlerInfoProvider
    {
        private readonly IHandlerMap handlerMap;

        public HandlerInfoProvider(IHandlerMap handlerMap)
        {
            this.handlerMap = handlerMap;
        }

        public Type GetHandlerType(Type inputType)
        {
            var handlerMethod = handlerMap.GetHandlerMethod(inputType);

            if (handlerMethod == null)
            {
                throw new FrameworkException($"Handler not found for input of type {inputType}.");
            }

            return handlerMethod.DeclaringType;
        }
    }
}
