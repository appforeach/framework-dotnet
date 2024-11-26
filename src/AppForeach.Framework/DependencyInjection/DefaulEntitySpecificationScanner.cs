﻿using AppForeach.Framework.DataType;
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
                if (type.BaseType != null && 
                    type.BaseType.IsGenericType && 
                    type.BaseType.GetGenericTypeDefinition() == typeof(BaseEntitySpecification<>))
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