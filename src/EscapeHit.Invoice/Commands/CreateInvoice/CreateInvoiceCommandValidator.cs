using AppForeach.Framework.FluentValidation;
using FluentValidation;

namespace EscapeHit.Invoice.Commands.CreateInvoice
{
    public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
    {
        public CreateInvoiceCommandValidator()
        {
            this.InheritFromEntitySpecification();
            RuleFor(x => x.CustomerNumber).NotNull().NotEmpty();
            RuleFor(x => x.CustomerNumber).MaximumLength(10);
        }
    }
}