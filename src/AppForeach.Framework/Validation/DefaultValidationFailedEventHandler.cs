using AppForeach.Framework.Logging;
using System.Collections.Generic;
using System.Text;

namespace AppForeach.Framework.Validation
{
    public class DefaultValidationFailedEventHandler : IValidationFailedEventHandler
    {
        private readonly IFrameworkLogger logger;
        private readonly IOperationContext context;

        public DefaultValidationFailedEventHandler(IFrameworkLogger logger, IOperationContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public void OnValidationFailed(OperationResult operationResult)
        {
            var outputState = context.State.Get<OperationOutputState>();
            var contextState = context.State.Get<OperationContextState>();

            StringBuilder error = new StringBuilder();

            for(int i = 0; i < outputState.Result.Errors.Count; i++)
            {
                var issue = outputState.Result.Errors[i];

                if(i > 0)
                {
                    error.AppendLine();
                }
                
                if (!string.IsNullOrEmpty(issue.Code))
                {
                    error.Append(issue.Code);
                    error.Append(" ");
                }

                if (!string.IsNullOrEmpty(issue.Field))
                {
                    error.Append(issue.Field);
                    error.Append(" ");
                }

                if (!string.IsNullOrEmpty(issue.Message))
                {
                    error.Append(issue.Message);
                }
                else
                {
                    error.Append("Empty validation message");
                }
            }

            logger.Log(FrameworkLogEvents.ValidationFailed, FrameworkLogLevel.Warning, "Validation failed", new Dictionary<string, object>
            {
                { FrameworkLogProperties.Logger, nameof(DefaultValidationFailedEventHandler) },
                { FrameworkLogProperties.OperationName, contextState.OperationName },
                { FrameworkLogProperties.ErrorMessage, error.ToString() }
            });
        }
    }
}
