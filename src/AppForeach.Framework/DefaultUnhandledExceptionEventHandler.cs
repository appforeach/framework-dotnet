using AppForeach.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppForeach.Framework
{
    public class DefaultUnhandledExceptionEventHandler : IUnhandledExceptionEventHandler
    {
        private readonly IFrameworkLogger logger;
        private readonly IOperationContext context;

        public DefaultUnhandledExceptionEventHandler(IFrameworkLogger logger, IOperationContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public ExceptionHandlerResult OnUnhandledException(Exception ex)
        {
            string errorId = Guid.NewGuid().ToString("n");
            var contextState = context.State.Get<OperationContextState>();

            StringBuilder fullStackTrace = new StringBuilder();
            Exception current = ex; 
            int exceptionIndex = 0;

            while(current != null)
            {
                fullStackTrace.AppendLine($"=== Exception { exceptionIndex } ===");

                fullStackTrace.Append("Type: ");
                fullStackTrace.AppendLine(current.GetType().FullName);

                fullStackTrace.Append("Message: ");
                fullStackTrace.AppendLine(current.Message);

                fullStackTrace.AppendLine("Stack Trace:");
                fullStackTrace.AppendLine(ex.StackTrace);

                fullStackTrace.AppendLine();

                current = current.InnerException;
                exceptionIndex++;
            }


            logger.Log(FrameworkLogEvents.UnhandledException, FrameworkLogLevel.Error, "Unhandled exception occured in execution", new Dictionary<string, object>
            {
                { FrameworkLogProperties.Logger, nameof(DefaultUnhandledExceptionEventHandler) },
                { FrameworkLogProperties.OperationName, contextState.OperationName },
                { FrameworkLogProperties.ErrorId, errorId },
                { FrameworkLogProperties.ErrorMessage, ex.Message },
                { FrameworkLogProperties.ErrorType, ex.GetType().FullName },
                { FrameworkLogProperties.ErrorStackTrace, fullStackTrace.ToString() },
            });

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
                            Message = "Unhandled exception occured",
                            State = new ExceptionOperationIssueState
                            {
                                OperationException = ex,
                                OperationExceptionId = errorId,
                            }
                        }
                    }
                }
            };
        }
    }
}
