using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AppForeach.Framework.DependencyInjection
{
    public class DefaultHandlerScanner : IComponentScanner
    {
        public IEnumerable<ComponentDefinition> ScanTypes(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                if (!type.Name.EndsWith("Handler") && !type.Name.EndsWith("UseCase"))
                {
                    continue;
                }
                
                var handlerMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

                foreach (var method in handlerMethods)
                {
                    var parameter = method.GetParameters().FirstOrDefault();

                    if (parameter == null)
                    {
                        continue;
                    }

                    string parameterTypeName = parameter.ParameterType.Name;

                    if (!parameterTypeName.EndsWith("Command") && !parameterTypeName.EndsWith("Query"))
                    {
                        continue;
                    }

                    var handlerDefintion = new HandlerDefinition
                    {
                        InputType = parameter.ParameterType,
                        ImplementationMethod = method
                    };

                    yield return new ComponentDefinition
                    {
                        ComponentType = typeof(IHandlerDefinition),
                        ImplementationInstance = handlerDefintion
                    };
                    
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
