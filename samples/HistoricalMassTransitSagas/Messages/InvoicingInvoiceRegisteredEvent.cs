using System;
using System.Collections.Generic;
using System.Text;

namespace HistoricalMassTransitSagas.Messages
{
    public class InvoicingInvoiceRegisteredEvent
    {
        public int InvoiceId { get; set; }
    }
}
