using AutoMapper;
using FluentValidation;

namespace EscapeHit.Invoice.Commands.CreateInvoice
{
    public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
    {
        public CreateInvoiceCommandValidator(IConfigurationProvider configurationProvider)
        {
            this.InheritFromMappingAndSpecification(configurationProvider);

            RuleFor(x => x.CustomerNumber).NotNull();
        }
    }
}

