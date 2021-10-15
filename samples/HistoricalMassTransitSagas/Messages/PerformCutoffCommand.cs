using System;

namespace HistoricalMassTransitSagas.Messages
{
    public class PerformCutoffCommand
    {
        public string AccountNumber { get; set; }

        public DateTime CutoffDate { get; set; }
    }
}
