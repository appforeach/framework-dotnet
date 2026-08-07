using System;

namespace AppForeach.Framework
{
    public interface ICompiledHandlerMap
    {
        CompiledHandlerInfo GetHandlerInfo(Type inputType);
    }
}
