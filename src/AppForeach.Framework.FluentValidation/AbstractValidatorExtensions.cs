using AppForeach.Framework.DataType.Facets;
using AppForeach.Framework.DataType;
using AppForeach.Framework.DependencyInjection;
using AppForeach.Framework.Mapping;

namespace FluentValidation;
public static class AbstractValidatorExtensions
{
    public static void InheritFromMappingAndSpecification<TCommand>(this AbstractValidator<TCommand> validator, IMappingMetadataProvider metadataProvider)
    {
        var mappingMetadata = metadataProvider.GetMappingMetadata(sourceType: GetCommandType());
        if (mappingMetadata is null)
            return;

        var entityType = mappingMetadata.DestinationType;
        if (entityType is null)
            return;

        var entitySpecification = FindSpecification(entityType);
        if (entitySpecification is null)
            return;


        foreach (var propertyMap in mappingMetadata.PropertyMaps)
        {
            if (entitySpecification.FieldSpecifications.TryGetValue(propertyMap.DestinationName, out var fieldSpecification))
            {
                var facets = fieldSpecification.Configuration;

                var requiredFacet = facets.TryGet<FieldRequiredFacet>();
                if (requiredFacet is not null)
                {
                    validator.RuleFor(propertyMap.SourceName).NotNull();
                }

                var maxLengthFacet = facets.TryGet<FieldMaxLengthFacet>();
                if (maxLengthFacet is not null)
                {
                    validator.RuleFor<TCommand, string>(propertyMap.SourceName).Length(maxLengthFacet.MaxLength);
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
