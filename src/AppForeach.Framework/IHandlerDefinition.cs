using System;
using System.Reflection;

namespace AppForeach.Framework
{
    public interface IHandlerDefinition
    {
        Type InputType { get; }

        MethodInfo ImplementationMethod { get; }
    }
}
