using System;
using System.Collections.Generic;
using System.Text;

namespace HistoricalMassTransitSagas.Messages
{
    public class InvoicingRegisterInvoiceCommand
    {
        public string AccountNumber { get; set; }

        public DateTime CutoffDate { get; set; }

        public decimal InvoiceAmount { get; set; }
    }
}
