using System;

namespace AppForeach.Framework.EntityFrameworkCore.Audit
{
    public class AuditEntity
    {
        public int Id { get; set; }

        public int? TransactionId { get; set; }

        public int? InputAuditId { get; set; }

        public string OperationName { get; set; }

        public bool IsCommand { get; set; }

        public bool IsInput { get; set; }

        public DateTimeOffset OccuredOn { get; set; }

        public string LoggingTraceId { get; set; }

        public string LoggingTransactionId { get; set; }

        public OperationOutcome? Outcome { get; set; }

        public string Type { get; set; }

        public string Payload { get; set; }
    }
}
