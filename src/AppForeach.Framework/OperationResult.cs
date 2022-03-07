using System.Collections.Generic;

namespace AppForeach.Framework
{
    public class OperationResult
    {
        public object Result { get; set; }

        public OperationOutcome Outcome { get; set; }

        public object OutcomeState { get; set; }

        public List<OperationIssue> Errors { get; set; } = new List<OperationIssue>();

        public List<OperationIssue> Warnings { get; set; } = new List<OperationIssue>();
    }
}
