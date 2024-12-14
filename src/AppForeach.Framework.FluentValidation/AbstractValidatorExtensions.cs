using AppForeach.Framework.DataType.Facets;
using AppForeach.Framework.DataType;
using AppForeach.Framework.DependencyInjection;
using FluentValidation;
using AutoMapper;

namespace FluentValidation;
public static class AbstractValidatorExtensions
{
    public static void InheritFromMappingAndSpecification<TCommand>(this AbstractValidator<TCommand> validator, IConfigurationProvider configurationProvider)
    {
        var mappingProvider = configurationProvider.GetAllTypeMaps()?.FirstOrDefault(x => x.SourceType == GetCommandType());
        if (mappingProvider is null)
            return;

        var mappingMetaData = mappingProvider.PropertyMaps.Where(x => x.HasSource);
        if (!mappingMetaData.Any())
            return;

        var entityType = mappingProvider.DestinationType;
        if (entityType is null)
            return;

        var entitySpecification = FindSpecification(entityType);
        if (entitySpecification is null)
            return;


        foreach (var mapping in mappingMetaData)
        {
            if (entitySpecification.FieldSpecifications.TryGetValue(mapping.SourceMember.Name, out var fieldSpecification))
            {
                var facets = fieldSpecification.Configuration;

                var requiredFacet = facets.TryGet<FieldRequiredFacet>();
                if (requiredFacet is not null)
                {
                    validator.RuleFor(mapping.SourceMember.Name).NotNull();
                }

                var maxLengthFacet = facets.TryGet<FieldMaxLengthFacet>();
                if (maxLengthFacet is not null)
                {
                    validator.RuleFor<TCommand, string>(mapping.SourceMember.Name).Length(maxLengthFacet.MaxLength);
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
            validator.GetType().BaseType.GetGenericArguments().FirstOrDefault();

    }
}
