
using AppForeach.Framework.DataType;

namespace EscapeHit.Invoice.Commands.CreateInvoice
{
    public interface ICreateInvoiceInputMapping
    {
        InvoiceEntity MapFrom(CreateInvoiceCommand input);
    }

    public class CreateInvoiceCommandMapping : BaseMapping<InvoiceEntity, CreateInvoiceCommand>, ICreateInvoiceInputMapping
    {
        public CreateInvoiceCommandMapping()
        {
            Field(e => e.CustomerNumber).From(e => e.CustomerNumber);
        }

        public InvoiceEntity MapFrom(CreateInvoiceCommand input) => BaseMap(input);
    }
}
