using AppForeach.Framework.DataType;
using AppForeach.Framework.DataType.Facets;
using AppForeach.Framework.FluentValidation.MetaData;
using FluentValidation;

namespace AppForeach.Framework.FluentValidation;

internal class ValidationApplicationService
    (
        ClassValidationMetadata existingValidation
    )
{
    public void ApplyToValidator<TCommand>(IPrimitiveFieldSpecification fieldSpecification, string propertyName, AbstractValidator<TCommand> destinationValidator)
    {
        var facets = fieldSpecification.Configuration;
        var existingValidators = existingValidation.GetValidators(propertyName);

        
        if (!existingValidators.Contains("NotNullValidator") && !existingValidators.Contains("NotEmptyValidator")
            && !existingValidators.Contains("NullValidator") && !existingValidators.Contains("EmptyValidator"))
        {
            var requiredFacet = facets.TryGet<FieldRequiredFacet>();
            bool isRequired = requiredFacet?.Required ?? false;

            var allowEmptyFacet = facets.TryGet<FieldIsEmptyAllowedFacet>();
            bool isEmptyAllowed = allowEmptyFacet?.IsEmptyAllowed ?? true;

            if (isRequired && isEmptyAllowed)
            {
                destinationValidator.RuleFor(propertyName).NotNull();
            }
            else if (isRequired && !isEmptyAllowed)
            {
                destinationValidator.RuleFor(propertyName).NotEmpty();
            }
            else if (!isRequired && !isEmptyAllowed)
            {
                destinationValidator.RuleFor<TCommand, string>(propertyName).SetValidator(new NotEmptyButMayBeNullValidator<TCommand, string>());
            }
        }

        var maxLengthFacet = facets.TryGet<FieldMaxLengthFacet>();
        if (maxLengthFacet is not null && !existingValidators.Contains("MaximumLengthValidator"))
        {
            destinationValidator.RuleFor<TCommand, string>(propertyName).MaximumLength(maxLengthFacet.MaxLength);
        }

        var precisionFacet = facets.TryGet<FieldPrecisionFacet>();
        if(precisionFacet is not null && !existingValidators.Contains("ScalePrecisionValidator"))
        {
            destinationValidator.RuleFor<TCommand, decimal>(propertyName).PrecisionScale(precisionFacet.Precision, precisionFacet.Scale, false);
        }
    }
}
