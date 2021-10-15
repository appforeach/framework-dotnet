using System;

namespace HistoricalMassTransitSagas.Messages
{
    public class RegisterInvoiceCommand
    {
        public string AccountNumber { get; set; }

        public DateTime CutoffDate { get; set; }

        public decimal InvoiceAmount { get; set; }
    }
}
