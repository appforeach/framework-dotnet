using AppForeach.Framework.DataType.Facets;
using AppForeach.Framework.DataType;
using AppForeach.Framework.DependencyInjection;
using AppForeach.Framework.Mapping;
using FluentValidation;
using AppForeach.Framework.FluentValidation.Exceptions;
using AppForeach.Framework.FluentValidation.MetaData;

namespace AppForeach.Framework.FluentValidation;
public static class AbstractValidatorExtensions
{
    private static List<Type> validatorsWithInheritanceFromSpecification = new List<Type>();

    public static void InheritFromEntitySpecification<TCommand>(this AbstractValidator<TCommand> validator)
    {
        validatorsWithInheritanceFromSpecification.Add(validator.GetType());
    }

    internal static bool IsValidatorInheritingFromMappingAndSpecification<TCommand>(this AbstractValidator<TCommand> validator)
        => validatorsWithInheritanceFromSpecification.Contains(validator.GetType());

    internal static void InheritOtherRulesFromSpecification<TCommand>(this AbstractValidator<TCommand> validator, AbstractValidator<TCommand> sourceValidator, IMappingMetadataProvider metadataProvider)
    {
        var overriddenValidationRules = ClassValidationMetadataBuilder.Build(sourceValidator.CreateDescriptor());

        var mappingMetadataCollection = metadataProvider.GetMappingMetadata(sourceType: GetCommandType());

        if (mappingMetadataCollection is null)
            return;

        if (!TryFindSpecification(mappingMetadataCollection, out BaseEntitySpecification entitySpecification, out IEnumerable<IPropertyMap> propertyMapBetweenCommandAndEntity))
            throw new UnableToMapCommandToSpecificationException($"Unable to map command {GetCommandType()} to specification");

        var validationApplicationService = new ValidationApplicationService(overriddenValidationRules);

        foreach (var propertyMap in propertyMapBetweenCommandAndEntity)
        {
            if (entitySpecification.FieldSpecifications.TryGetValue(propertyMap.DestinationName, out var fieldSpecification))
            {
                validationApplicationService.ApplyToValidator(fieldSpecification, propertyMap.SourceName, validator);
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