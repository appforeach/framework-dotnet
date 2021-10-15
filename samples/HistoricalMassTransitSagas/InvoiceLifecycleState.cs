using System;
using System.Collections.Generic;
using System.Text;

namespace HistoricalMassTransitSagas
{
    class InvoiceLifecycleState
    {
        public Guid CorrelationId { get; set; }

        public string AccountNumber { get; set; }

        public DateTime CutoffDate { get; set; }

        public decimal InvoiceAmount { get; set; }

        public int InvoiceId { get; set; }
    }
}
