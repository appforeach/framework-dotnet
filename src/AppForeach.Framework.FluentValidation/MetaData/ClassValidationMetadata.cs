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

    internal bool HasRequiredValidator(string propertyName)
    {
        var propertyMetadata = PropertyValidators.FirstOrDefault(x => x.Name == propertyName);

        if (propertyMetadata == null)
            return false;

        return 
            propertyMetadata.Validators.Contains("NotNullValidator") || 
            propertyMetadata.Validators.Contains("NotEmptyValidator");
    }

    internal bool HasMaximumLengthValidator(string propertyName)
    {
        var propertyMetadata = PropertyValidators.FirstOrDefault(x => x.Name == propertyName);

        if (propertyMetadata == null)
            return false;

        return
            propertyMetadata.Validators.Contains("MaximumLengthValidator");
    }
}