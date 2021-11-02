using System;
using System.Reflection;

namespace AppForeach.Framework
{
    public interface IHandlerMap
    {
        MethodInfo GetHandlerMethod(Type inputType);
    }
}
