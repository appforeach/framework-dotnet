using System.Diagnostics.CodeAnalysis;

namespace AppForeach.Framework.FluentValidation.MetaData;
internal class ClassValidationMetadata
{
    [SetsRequiredMembers]
    internal ClassValidationMetadata(IEnumerable<PropertyValidationMetadata> propertyValidators)
    {
        this.PropertyValidators = propertyValidators;
    }

    internal required IEnumerable<PropertyValidationMetadata> PropertyValidators { get; init; }

    public IEnumerable<string> GetValidators(string propertyName)
    {
        var propertyMetadata = PropertyValidators.FirstOrDefault(x => x.Name == propertyName);

        if (propertyMetadata == null)
        {
            return Enumerable.Empty<string>();
        }

        return propertyMetadata.Validators;
    }
}