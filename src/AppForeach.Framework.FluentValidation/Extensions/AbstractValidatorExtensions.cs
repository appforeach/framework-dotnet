using AppForeach.Framework.DataType.Facets;
using AppForeach.Framework.DataType;
using AppForeach.Framework.DependencyInjection;
using AppForeach.Framework.Mapping;
using FluentValidation;
using AppForeach.Framework.FluentValidation.Exceptions;

namespace AppForeach.Framework.FluentValidation.Extensions;
public static class AbstractValidatorExtensions
{
    public static void InheritFromMappingAndSpecification<TCommand>(this AbstractValidator<TCommand> validator, IMappingMetadataProvider metadataProvider, Action<ValidationOptions<TCommand>>? actionWithOptions = null)
    {
        var validationOptions = ValidationOptions<TCommand>.Default();

        if (actionWithOptions is not null)
            actionWithOptions(validationOptions);

        var mappingMetadataCollection = metadataProvider.GetMappingMetadata(sourceType: GetCommandType());

        if (mappingMetadataCollection is null)
            return;

        if (!TryFindSpecification(mappingMetadataCollection, out BaseEntitySpecification entitySpecification, out IEnumerable<IPropertyMap> propertyMapBetweenCommandAndEntity))
            throw new UnableToMapCommandToSpecificationException($"Unable to map command {GetCommandType()} to specification");

        foreach (var propertyMap in propertyMapBetweenCommandAndEntity)
        {
            if (entitySpecification.FieldSpecifications.TryGetValue(propertyMap.DestinationName, out var fieldSpecification))
            {
                if (validationOptions.ShouldSkip(propertyMap.SourceName))
                    continue;

                var facets = fieldSpecification.Configuration;

                var requiredFacet = facets.TryGet<FieldRequiredFacet>();
                if (requiredFacet is not null)
                {
                    validator.RuleFor(propertyMap.SourceName).NotNull().When(x => true);
                }

                var maxLengthFacet = facets.TryGet<FieldMaxLengthFacet>();
                if (maxLengthFacet is not null)
                {
                    validator.RuleFor<TCommand, string>(propertyMap.SourceName).Length(maxLengthFacet.MaxLength);
                }
            }
        }


        bool TryFindSpecification(IEnumerable<IMappingMetadata> mappingMetadataList, out BaseEntitySpecification specification, out IEnumerable<IPropertyMap> propertyMaps)
        {
            var scanner = new DefaulEntitySpecificationScanner();
            specification = null!;
            propertyMaps = null!;

            bool oneSpecificationAlreadyFound = false;

            foreach (var mappingMetadata in mappingMetadataList)
            {
                var entityType = mappingMetadata.DestinationType;
                var entitySpecificationTypeToFind = typeof(BaseEntitySpecification<>).MakeGenericType(mappingMetadata.DestinationType);

                var scannedDefinition = scanner
                    .ScanTypes(entityType.Assembly.GetTypes())
                    .FirstOrDefault(t => entitySpecificationTypeToFind.IsAssignableFrom(t.ComponentType));

                if (scannedDefinition is not null)
                {
                    specification = (BaseEntitySpecification)Activator.CreateInstance(scannedDefinition.ComponentType)!;
                    propertyMaps = mappingMetadata.PropertyMaps;

                    if (oneSpecificationAlreadyFound)
                        throw new MultipleSpecificationsPerCommandFoundException($"Multiple specifications per command {GetCommandType()} found");
                    oneSpecificationAlreadyFound = true;
                }
            }

            return oneSpecificationAlreadyFound;
        }

        Type GetCommandType() =>
            validator?.GetType()?.BaseType?.GetGenericArguments()?.FirstOrDefault()!;

    }

    // this is very dirty of couse but just for prototype
    private static List<Type> validatorsWithInheritanceFromSpecification = new List<Type>();

    public static void InheritFromMappingAndSpecification<TCommand>(this AbstractValidator<TCommand> validator)
    {
        validatorsWithInheritanceFromSpecification.Add(validator.GetType());
    }

    public static bool IsValidatorInheritingFromMappingAndSpecification<TCommand>(this AbstractValidator<TCommand> validator)
        => validatorsWithInheritanceFromSpecification.Contains(validator.GetType());

    public static void InheritOtherRulesFromSpecification<TCommand>(this AbstractValidator<TCommand> validator, AbstractValidator<TCommand> sourceValidator, IMappingMetadataProvider metadataProvider)
    {
        var overridenProperties = sourceValidator.CreateDescriptor().Rules.Select(x => x.PropertyName).Distinct();

        var validationOptions = ValidationOptions<TCommand>.Default();

        var mappingMetadataCollection = metadataProvider.GetMappingMetadata(sourceType: GetCommandType());

        if (mappingMetadataCollection is null)
            return;

        if (!TryFindSpecification(mappingMetadataCollection, out BaseEntitySpecification entitySpecification, out IEnumerable<IPropertyMap> propertyMapBetweenCommandAndEntity))
            throw new UnableToMapCommandToSpecificationException($"Unable to map command {GetCommandType()} to specification");

        foreach (var propertyMap in propertyMapBetweenCommandAndEntity)
        {
            if (entitySpecification.FieldSpecifications.TryGetValue(propertyMap.DestinationName, out var fieldSpecification))
            {
                // skip overriden rules
                if (overridenProperties.Contains(propertyMap.SourceName))
                    continue;

                var facets = fieldSpecification.Configuration;

                var requiredFacet = facets.TryGet<FieldRequiredFacet>();
                if (requiredFacet is not null)
                {
                    validator.RuleFor(propertyMap.SourceName).NotNull().When(x => true);
                }

                var maxLengthFacet = facets.TryGet<FieldMaxLengthFacet>();
                if (maxLengthFacet is not null)
                {
                    validator.RuleFor<TCommand, string>(propertyMap.SourceName).Length(maxLengthFacet.MaxLength);
                }
            }
        }


        bool TryFindSpecification(IEnumerable<IMappingMetadata> mappingMetadataList, out BaseEntitySpecification specification, out IEnumerable<IPropertyMap> propertyMaps)
        {
            var scanner = new DefaulEntitySpecificationScanner();
            specification = null!;
            propertyMaps = null!;

            bool oneSpecificationAlreadyFound = false;

            foreach (var mappingMetadata in mappingMetadataList)
            {
                var entityType = mappingMetadata.DestinationType;
                var entitySpecificationTypeToFind = typeof(BaseEntitySpecification<>).MakeGenericType(mappingMetadata.DestinationType);

                var scannedDefinition = scanner
                    .ScanTypes(entityType.Assembly.GetTypes())
                    .FirstOrDefault(t => entitySpecificationTypeToFind.IsAssignableFrom(t.ComponentType));

                if (scannedDefinition is not null)
                {
                    specification = (BaseEntitySpecification)Activator.CreateInstance(scannedDefinition.ComponentType)!;
                    propertyMaps = mappingMetadata.PropertyMaps;

                    if (oneSpecificationAlreadyFound)
                        throw new MultipleSpecificationsPerCommandFoundException($"Multiple specifications per command {GetCommandType()} found");
                    oneSpecificationAlreadyFound = true;
                }
            }

            return oneSpecificationAlreadyFound;
        }

        Type GetCommandType() =>
            validator?.GetType()?.BaseType?.GetGenericArguments()?.FirstOrDefault()!;

    }
}