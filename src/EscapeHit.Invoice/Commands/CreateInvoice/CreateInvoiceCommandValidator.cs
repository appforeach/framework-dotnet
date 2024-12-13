using AppForeach.Framework.DataType.Facets;
using AppForeach.Framework.DataType;
using AppForeach.Framework.DependencyInjection;
using AutoMapper;
using FluentValidation;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace EscapeHit.Invoice.Commands.CreateInvoice
{
    public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
    {
        private readonly IConfigurationProvider configurationProvider;
        public CreateInvoiceCommandValidator(IConfigurationProvider configurationProvider)
        {
            this.configurationProvider = configurationProvider;

            InheritFromMappingAndSpecification();

            RuleFor(x => x.CustomerNumber).NotNull();
        }

        protected void InheritFromMappingAndSpecification()
        {
            var typeMap = configurationProvider.GetAllTypeMaps()?.FirstOrDefault(x => x.SourceType == GetCommandType());
            if (typeMap is null)
                return;

            var propertyMaps = typeMap.PropertyMaps.Where(x => x.HasSource);
            if (!propertyMaps.Any())
                return;

            var entityType = typeMap.DestinationType;
            if (entityType is null)
                return;

            var entitySpecification = FindSpecification(entityType);
            if (entitySpecification is null)
                return;


            foreach (var propertyMap in propertyMaps)
            {
                if (entitySpecification.FieldSpecifications.TryGetValue(propertyMap.SourceMember.Name, out var fieldSpecification))
                {
                    var facets = fieldSpecification.Configuration;

                    var requiredFacet = facets.TryGet<FieldRequiredFacet>();
                    if (requiredFacet is not null)
                    {
                        this.RuleFor(propertyMap.SourceMember.Name).NotNull();
                    }

                    var maxLengthFacet = facets.TryGet<FieldMaxLengthFacet>();
                    if (maxLengthFacet is not null)
                    {
                        this.RuleForProperty<CreateInvoiceCommand, string>(propertyMap.SourceMember.Name).Length(maxLengthFacet.MaxLength);
                    }
                }
            }


            static BaseEntitySpecification FindSpecification(Type entityType)
            {
                var scanner = new DefaulEntitySpecificationScanner();

                var entitySpecificationTypeToFind = typeof(BaseEntitySpecification<>).MakeGenericType(entityType);

                var scannedDefinition = scanner
                    .ScanTypes(entityType.Assembly.GetTypes())
                    .FirstOrDefault(t => entitySpecificationTypeToFind.IsAssignableFrom(t.ComponentType));

                if (scannedDefinition is not null)
                {
                    var entitySpecification = (BaseEntitySpecification)Activator.CreateInstance(scannedDefinition.ComponentType);

                    return entitySpecification;
                }

                return null;
            }

            Type GetCommandType() => 
                this.GetType().BaseType.GetGenericArguments().FirstOrDefault();
           
        }
    }

    //TODO: move to framework
    public static class ValidatorExtensions
    {
        public static IRuleBuilderInitial<T, TProperty> RuleFor<T, TProperty>(this AbstractValidator<T> validator, string propertyName)
        {
            return validator.RuleFor<TProperty>(x => GetPropertyValue<TProperty>(x, propertyName));
        }

        private static TProperty GetPropertyValue<TProperty>(object obj, string propertyName)
        {
            var propertyInfo = obj.GetType().GetProperty(propertyName);
            return (TProperty)propertyInfo?.GetValue(obj, null);
        }

        public static IRuleBuilderInitial<T, object> RuleFor<T>(this AbstractValidator<T> validator, string propertyName)
        {
            return validator.RuleFor<object>(x => GetPropertyValue(x, propertyName));
        }

        private static object GetPropertyValue(object obj, string propertyName)
        {
            var propertyInfo = obj.GetType().GetProperty(propertyName);
            return propertyInfo?.GetValue(obj, null);
        }


        public static IRuleBuilderInitial<T, TProperty> RuleForProperty<T, TProperty>(
               this AbstractValidator<T> validator,
               string propertyName)
        {
            var propertyInfo = typeof(T).GetProperty(propertyName);

            if (propertyInfo == null)
            {
                throw new ArgumentException($"Property '{propertyName}' not found on type '{typeof(T).Name}'.");
            }

            // Build a lambda expression for the property
            var parameter = Expression.Parameter(typeof(T), "x");
            var propertyAccess = Expression.MakeMemberAccess(parameter, propertyInfo);
            var lambda = Expression.Lambda<Func<T, TProperty>>(propertyAccess, parameter);

            return validator.RuleFor(lambda);
        }
    }
}