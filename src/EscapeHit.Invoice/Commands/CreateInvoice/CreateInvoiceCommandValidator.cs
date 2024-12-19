using AppForeach.Framework.Mapping;
using AutoMapper;
using FluentValidation;

namespace EscapeHit.Invoice.Commands.CreateInvoice
{
    public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
    {
        public CreateInvoiceCommandValidator(IMappingMetadataProvider metadataProvider)
        {
            this.InheritFromMappingAndSpecification(metadataProvider);

            RuleFor(x => x.CustomerNumber).NotNull();
        }
    }
}

