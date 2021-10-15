using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkSagas
{
    public class InvoiceRegistrationActivityState
    { 
        public Guid CorrelationId { get; set ; }

        public string AccountNumber { get; set; }

        public DateTime CutoffDate { get; set; }

        public decimal InvoiceAmount { get; set; }

        public int InvoiceId { get; set; }
    }
}
