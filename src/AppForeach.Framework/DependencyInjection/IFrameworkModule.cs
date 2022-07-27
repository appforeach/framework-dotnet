using System.Collections.Generic;

namespace AppForeach.Framework.DependencyInjection
{
    public interface IFrameworkModule
    {
        IEnumerable<ComponentDefinition> Components { get; }

        ComponentLifetime? AssemblyDefaultLifetime { get; }
    }
}
