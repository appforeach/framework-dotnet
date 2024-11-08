
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
            //todo: implement declarative mapping
            Field(e => e.CustomerNumber).From(e => e.CustomerNumber);
            Field(e => e.Amount).From(e => e.Amount);
        }

        public InvoiceEntity MapFrom(CreateInvoiceCommand input)
        {
            var output = new InvoiceEntity();

            output.CustomerNumber = input.CustomerNumber;
            output.Amount = input.Amount;

            return output;
        }
    }
}
