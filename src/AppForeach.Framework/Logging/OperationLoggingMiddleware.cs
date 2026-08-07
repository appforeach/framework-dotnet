using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AppForeach.Framework.Logging
{
    public class OperationLoggingMiddleware : IOperationMiddleware
    {
        private readonly IFrameworkLogger logger;
        private readonly IOperationContext context;

        public OperationLoggingMiddleware(IFrameworkLogger logger, IOperationContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public async Task ExecuteAsync(NextOperationDelegate next, CancellationToken ct)
        {
            long start = Stopwatch.GetTimestamp();
            
            try
            {
                await next();
            }
            finally
            {
                double elapsed = (Stopwatch.GetTimestamp() - start) * 1000 / (double)Stopwatch.Frequency;

                var outputState = context.State.Get<OperationOutputState>();
                var contextState = context.State.Get<OperationContextState>();

                var properties = new Dictionary<string, object>
                {
                    { FrameworkLogProperties.Logger, nameof(OperationLoggingMiddleware) },
                    { FrameworkLogProperties.OperationOutcome, outputState.Result?.Outcome.ToString() },
                    { FrameworkLogProperties.OperationDuration, elapsed },
                };

                if(contextState.IsOperationNameResolved)
                {
                    properties[FrameworkLogProperties.OperationName] = contextState.OperationName;
                    properties[FrameworkLogProperties.OperationKind] = context.IsCommand ? "Command" : "Query";
                }

                logger.Log(FrameworkLogEvents.OperationCompleted, FrameworkLogLevel.Information, "Operation completed", properties);
            }
        }
    }
}
