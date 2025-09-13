
namespace EscapeHit.Invoice.Commands.CreateInvoice
{
    public class CreateInvoiceCommand
    {
        public string CustomerNumber { get; set; }

        public decimal Amount { get; set; }
    }
}
