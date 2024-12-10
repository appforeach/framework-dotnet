using FluentValidation;

namespace EscapeHit.Invoice.Commands.CreateInvoice
{
    public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
    {
        public CreateInvoiceCommandValidator()
        {
            //InheritFromMappingAndSpecification();

            RuleFor(x => x.CustomerNumber).NotNull();
        }
    }
}
