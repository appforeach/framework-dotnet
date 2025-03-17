using AppForeach.Framework.FluentValidation.Extensions;
using AppForeach.Framework.Mapping;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForeach.Framework.FluentValidation
{
    public class FluentValidatorSpecificationValidator<TValidationInput, TFluentValidator> : AbstractValidator<TValidationInput>
        where TFluentValidator: AbstractValidator<TValidationInput>
    {
        public FluentValidatorSpecificationValidator(TFluentValidator validator, IMappingMetadataProvider metadataProvider)
        {
            Include(validator);

            if (validator.IsValidatorInheritingFromMappingAndSpecification())
            {
                this.InheritOtherRulesFromSpecification(validator, metadataProvider);
            }
        }
    }
}
