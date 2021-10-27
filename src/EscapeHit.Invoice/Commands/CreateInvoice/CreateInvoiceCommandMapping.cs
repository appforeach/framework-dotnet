
namespace EscapeHit.Invoice.Commands.CreateInvoice
{
    public interface ICreateInvoiceInputMapping
    {
        InvoiceEntity MapFrom(CreateInvoiceCommand input);
    }

    public class CreateInvoiceCommandMapping : ICreateInvoiceInputMapping
    {
        public InvoiceEntity MapFrom(CreateInvoiceCommand input)
        {
            var entity = new InvoiceEntity();

            entity.CustomerNumber = input.CustomerNumber;
            entity.Amount = input.Amount;

            return entity;
        }
    }
}
