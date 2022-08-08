
using EscapeHit.Invoice.Services;

namespace EscapeHit.Invoice
{
    public class InvoiceComponents : ComponentModule
    {
        public InvoiceComponents()
        {
            AssemblyDefaultLifetimeTransient();
        }
    }
}
