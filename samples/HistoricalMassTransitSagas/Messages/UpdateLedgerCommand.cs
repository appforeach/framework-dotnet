using System;
using System.Collections.Generic;
using System.Text;

namespace HistoricalMassTransitSagas.Messages
{
    public class UpdateLedgerCommand
    {
        public string AccountNumber { get; set; }

        public string Transasction { get; set; }

        public decimal Amount { get; set; }
    }
}
