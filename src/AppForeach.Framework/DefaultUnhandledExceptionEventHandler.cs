using System;
using System.Collections.Generic;

namespace AppForeach.Framework
{
    public class DefaultUnhandledExceptionEventHandler : IUnhandledExceptionEventHandler
    {
        public ExceptionHandlerResult OnUnhandledException(Exception ex)
        {
            return new ExceptionHandlerResult
            {
                IsHandled = true,
                Result = new OperationResult
                {
                    Outcome = OperationOutcome.Error,
                    Errors = new List<OperationIssue>
                    {
                        new OperationIssue
                        {
                            Code = "UnhandledException",
                            Message = "Unhandled exception occured"
                        }
                    }
                }
            };
        }
    }
}
