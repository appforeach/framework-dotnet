
using EscapeHit.Invoice.Services;

namespace EscapeHit.Invoice
{
    public class InvoiceComponents : ComponentRegistration
    {
        public InvoiceComponents()
        {
            SetDefaultTransient();

            AddSingleton<IInvoiceNumberService, InvoiceNumberService>();
        }
    }
}
