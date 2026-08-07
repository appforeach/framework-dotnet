using System;

namespace AppForeach.Framework
{
    public interface IHandlerInfoProvider
    {
        Type GetHandlerType(Type inputType);
    }
}
