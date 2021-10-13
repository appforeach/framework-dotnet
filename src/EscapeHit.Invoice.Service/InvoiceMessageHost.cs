using EscapeHit.Invoice.Commands.CreateInvoice;
using EscapeHit.Service;

namespace EscapeHit.Invoice.Service
{
    public class InvoiceMessageHost : MessageHost
    {
        public InvoiceMessageHost()
        {
            Consume<CreateInvoiceInput>();
        }
    }
}
