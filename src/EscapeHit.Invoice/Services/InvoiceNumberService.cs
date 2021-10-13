using System;

namespace EscapeHit.Invoice.Services
{
    public interface IInvoiceNumberService
    {
        string GenerateInvoiceNumber(InvoiceEntity invoice);
    }

    public class InvoiceNumberService : IInvoiceNumberService
    {
        public string GenerateInvoiceNumber(InvoiceEntity invoice)
        {
            throw new NotImplementedException();
        }
    }
}
