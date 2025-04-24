using FluentValidation;

namespace AppForeach.Framework.FluentValidation.MetaData;
internal static class ClassValidationMetadataBuilder
{
    internal static ClassValidationMetadata Build(IValidatorDescriptor validatorDescriptor)
    {
        var propertyValidationMetadata = validatorDescriptor.GetMembersWithValidators().Select(key => new PropertyValidationMetadata { Name = key.Key, Validators = key.Select(y => y.Validator.Name).ToList() }).ToList();
        return new ClassValidationMetadata(propertyValidationMetadata);
    }
}
