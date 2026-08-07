using System;

namespace AppForeach.Framework
{
    internal class CompiledHandlerInfoProvider : IHandlerInfoProvider
    {
        private readonly ICompiledHandlerMap compiledHandlerMap;

        public CompiledHandlerInfoProvider(ICompiledHandlerMap compiledHandlerMap)
        {
            this.compiledHandlerMap = compiledHandlerMap;
        }

        public Type GetHandlerType(Type inputType)
        {
            var handlerInfo = compiledHandlerMap.GetHandlerInfo(inputType)
                ?? throw new FrameworkException($"Handler not found for input of type {inputType}.");

            return handlerInfo.HandlerType;
        }
    }
}
