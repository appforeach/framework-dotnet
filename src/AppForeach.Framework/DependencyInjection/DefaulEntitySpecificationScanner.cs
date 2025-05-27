using AppForeach.Framework.DataType;
using System;
using System.Collections.Generic;

namespace AppForeach.Framework.DependencyInjection
{
    public class DefaulEntitySpecificationScanner : IComponentScanner
    {
        public IEnumerable<ComponentDefinition> ScanTypes(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                if (typeof(BaseEntitySpecification).IsAssignableFrom(type)
                    && ! type.ContainsGenericParameters)
                {
                    yield return new ComponentDefinition
                    {
                        ComponentType = type,
                        ImplementationType = type,
                        Lifetime = ComponentLifetime.Transient
                    };
                }
            }
        }
    }
}