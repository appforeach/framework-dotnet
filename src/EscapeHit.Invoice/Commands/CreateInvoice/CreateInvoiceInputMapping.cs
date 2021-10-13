
namespace EscapeHit.Invoice.Commands.CreateInvoice
{
    public interface ICreateInvoiceInputMapping
    {
        InvoiceEntity MapFrom(CreateInvoiceInput input);
    }

    public class CreateInvoiceInputMapping : ICreateInvoiceInputMapping
    {
        public InvoiceEntity MapFrom(CreateInvoiceInput input)
        {
            var entity = new InvoiceEntity();

            entity.CustomerNumber = input.CustomerNumber;
            entity.Amount = input.Amount;

            return entity;
        }
    }
}
