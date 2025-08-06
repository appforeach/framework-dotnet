
using EscapeHit.Invoice.Services;

namespace EscapeHit.Invoice
{
    public class InvoiceComponents : EscapeHitComponentModule
    {
        public InvoiceComponents()
        {
            AssemblyDefaultLifetimeTransient();
        }
    }
}
