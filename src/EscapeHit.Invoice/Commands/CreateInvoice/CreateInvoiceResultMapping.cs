
namespace EscapeHit.Invoice.Commands.CreateInvoice
{
    public interface ICreateInvoiceOutputMapping
    {
        CreateInvoiceResult MapFrom(InvoiceEntity entity);
    }

    public class CreateInvoiceResultMapping : ICreateInvoiceOutputMapping
    {
        public CreateInvoiceResult MapFrom(InvoiceEntity entity)
        {
            var output = new CreateInvoiceResult();
            
            output.InvoiceId = entity.Id;
            output.InvoiceNumber = entity.Number;
            
            return output;
        }
    }
}
