using System;
using System.Collections.Generic;
using System.Text;

namespace HistoricalMassTransitSagas.Messages
{
    class InvoicingCloseInvoiceCommand
    {
        public int InvoiceId { get; set; }
    }
}
