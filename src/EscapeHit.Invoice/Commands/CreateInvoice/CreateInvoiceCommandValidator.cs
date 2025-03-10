using AppForeach.Framework.FluentValidation.Extensions;
using AppForeach.Framework.Mapping;
using FluentValidation;

namespace EscapeHit.Invoice.Commands.CreateInvoice
{
    public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
    {
        public CreateInvoiceCommandValidator(IMappingMetadataProvider metadataProvider)
        {
            /*
                Original Idea:
                - 1 put marker while calling InheritRulesFromSpecification
                - 2 Create a validator and set rules defined via RuleFor(x => x.CustomerNumber)
                - 3 If there is a marker, then inherit all rules from the spec except the ones defined in the validator
             */



            // This solution is based on a temporary coupling. Some analyer is needed to warn the developers
            RuleFor(x => x.CustomerNumber).MaximumLength(2);
            this.InheritOtherRulesFromSpecification(metadataProvider);
        }
    }
}