using System;
using System.Collections.Generic;
using System.Text;

namespace HistoricalMassTransitSagas.Messages
{
    public class InvoiceRegisteredEvent
    {
        public int InvoiceId { get; set; }
    }
}
