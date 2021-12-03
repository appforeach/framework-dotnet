using AppForeach.Framework.DataType;

namespace EscapeHit.Invoice.Commands.CreateInvoice
{
    public class CreateInvoiceValidation : BaseValidation<CreateInvoiceCommand>
    {
        public CreateInvoiceValidation()
        {
            InheritFromMappingAndSpecification();

            Field(e => e.CustomerNumber).IsOptional();
        }
    }
}
