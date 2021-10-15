using System;

namespace HistoricalMassTransitSagas.Messages
{
    public class AccountInvoceRegistrationEvent
    {
        public string AccountNumber { get; set; }

        public DateTime CutoffDate { get; set; }
    }
}
