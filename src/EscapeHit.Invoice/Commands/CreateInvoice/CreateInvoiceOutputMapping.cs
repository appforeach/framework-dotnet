
namespace EscapeHit.Invoice.Commands.CreateInvoice
{
    public interface ICreateInvoiceOutputMapping
    {
        CreateInvoiceOutput MapFrom(InvoiceEntity entity);
    }

    public class CreateInvoiceOutputMapping : ICreateInvoiceOutputMapping
    {
        public CreateInvoiceOutput MapFrom(InvoiceEntity entity)
        {
            var output = new CreateInvoiceOutput();
            
            output.InvoiceId = entity.Id;
            output.InvoiceNumber = entity.Number;
            
            return output;
        }
    }
}
