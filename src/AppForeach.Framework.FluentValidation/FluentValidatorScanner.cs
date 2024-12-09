using AppForeach.Framework.DependencyInjection;
using AppForeach.Framework.Validation;
using FluentValidation;

namespace AppForeach.Framework.FluentValidation
{
    public class FluentValidatorScanner : IComponentScanner
    {
        public IEnumerable<ComponentDefinition> ScanTypes(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                Type? baseType;

                if ((baseType = type.BaseType) != null && baseType.IsAbstract && baseType.IsGenericType
                    && baseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>))
                {
                    Type validatorInputType = baseType.GetGenericArguments()[0];

                    Type fluentValidatorProxyType = typeof(FluentValidatorProxy<>).MakeGenericType(type);

                    yield return new ComponentDefinition
                    {
                        ComponentType = type,
                        ImplementationType = type,
                        Lifetime = ComponentLifetime.Scoped
                    };

                    yield return new ComponentDefinition
                    {
                        ComponentType = fluentValidatorProxyType,
                        ImplementationType = fluentValidatorProxyType,
                        Lifetime = ComponentLifetime.Scoped
                    };

                    yield return new ComponentDefinition
                    {
                        ComponentType = typeof(IValidatorDefinition),
                        ImplementationInstance = new ValidatorDefinition
                        { 
                            InputType = validatorInputType,
                            ValidatorType = fluentValidatorProxyType
                        }
                    };
                }
            }
        }
    }
}
