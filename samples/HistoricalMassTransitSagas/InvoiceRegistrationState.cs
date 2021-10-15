using System;
using Automatonymous;

namespace HistoricalMassTransitSagas
{
    public class InvoiceRegistrationState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set ; }

        public string AccountNumber { get; set; }

        public DateTime CutoffDate { get; set; }

        public decimal InvoiceAmount { get; set; }

        public int InvoiceId { get; set; }
    }
}
