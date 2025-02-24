using AppForeach.Framework.FluentValidation.Extensions;
using AppForeach.Framework.Mapping;
using FluentValidation;

namespace EscapeHit.Invoice.Commands.CreateInvoice
{
    public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
    {
        public CreateInvoiceCommandValidator(IMappingMetadataProvider metadataProvider)
        {
            // an example of how to skip validation at specification level
            // this.InheritFromMappingAndSpecification(metadataProvider, options => options.Skip(x=>x.CustomerNumber));

            this.InheritFromMappingAndSpecification(metadataProvider);

            // an example of how to enrich with custom validation beside specification
            // RuleFor(x => x.CustomerNumber).MaximumLength(100);
        }
    }
}