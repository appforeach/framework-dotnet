using System;

namespace AppForeach.Framework.DependencyInjection
{
    public class ComponentDefinition
    {
        public Type ComponentType { get; set; }

        public ComponentLifetime Lifetime { get; set; }

        public Type ImplementationType { get; set; }

        public Func<IServiceLocator, object> ImplementationFunction { get; set; }

        public object ImplementationInstance { get; set; }

        public bool IsOptional { get; set; } = false;
    }
}
