using System;
using System.Collections.Generic;

namespace AppForeach.Framework.DependencyInjection
{
    public interface IComponentScanner
    {
        IEnumerable<ComponentDefinition> ScanTypes(IEnumerable<Type> types);
    }
}
