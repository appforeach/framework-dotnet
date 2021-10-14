using System;
using System.Collections.Generic;
using System.Text;

namespace HistoricalBusinessScope
{
    public class InvoiceServiceSimplified
    {
        private readonly InvoiceMapping mapping;
        private readonly InvoiceRepository invoiceRepository;

        public InvoiceServiceSimplified(InvoiceMapping mapping, InvoiceRepository invoiceRepository)
        {
            this.mapping = mapping;
            this.invoiceRepository = invoiceRepository;
        }

        public CreateInvoiceResponse CreateInvoice(CreateInvoiceRequest request)
        {
            Invoice invoice = mapping.Map(request);

            int invoiceId = invoiceRepository.Create(invoice);

            return new CreateInvoiceResponse(invoiceId);
        }
    }
}
