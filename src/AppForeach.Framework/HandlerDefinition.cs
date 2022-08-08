using System;
using System.Reflection;

namespace AppForeach.Framework
{
    public class HandlerDefinition : IHandlerDefinition
    {
        public Type InputType { get; set; }

        public MethodInfo ImplementationMethod { get; set; }
    }
}
