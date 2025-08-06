using AppForeach.Framework.Hosting.Features.Logging;
using AppForeach.Framework.Logging;
using Elastic.CommonSchema;
using Elastic.CommonSchema.Serilog;
using Serilog.Events;
using Serilog.Formatting;

namespace AppForeach.Framework.Hosting.Features.Serilog.Ecs
{
    public class EcsSerilogTextFormatter : ITextFormatter
    {
        private readonly EcsTextFormatterConfiguration ecsConfiguration = new();
        private readonly ILoggingPropertyMapAggregator mapAggregator;

        private static readonly HashSet<string> ecsNotExpandableProperties =
            ["ecs.version", "span.id", "trace.id", "transaction.id"];

        private static readonly JsonPropertyConverter jsonConverter = new EcsJsonPropertyConverter(ecsNotExpandableProperties);

        public EcsSerilogTextFormatter()
        {
            this.mapAggregator = new EmptyLoggingPropertyMapAggregator();
        }

        public EcsSerilogTextFormatter(ILoggingPropertyMapAggregator mapAggregator)
        {
            this.mapAggregator = mapAggregator;
        }

        public void Format(LogEvent logEvent, TextWriter output)
        {
            var ecsEvent = LogEventConverter.ConvertToEcs(logEvent, ecsConfiguration);

            var headerProperties = GetHeaderProperties(logEvent, ecsEvent);

            var requiredProperties = GetRequiredProperties(ecsEvent);

            var additionalProperties = GetAdditionalProperties(logEvent);

            var mappedProperties = mapAggregator.MapProperties(requiredProperties.Concat(additionalProperties)
                .Select(t => new KeyValuePair<string, object>(t.Item1, t.Item2)));

            var resultProperties = mappedProperties.GroupBy(p => p.Key).OrderBy(g => g.Key)
                .Select(g => (g.Key, g.Last().Value));

            output.WriteLine(jsonConverter.GetJson(headerProperties.Concat(resultProperties)));
        }

        private static IEnumerable<(string, object)> GetHeaderProperties(LogEvent logEvent, EcsDocument ecsEvent)
        {
            List<(string, object)> properties = new();

            properties.Add(("@timestamp", logEvent.Timestamp));
            properties.Add(("message", logEvent.RenderMessage()));
            properties.Add(("ecs.version", "8.17.0"));

            if (ecsEvent.TransactionId != null)
            {
                properties.Add(("transaction.id", ecsEvent.TransactionId));
            }

            if (ecsEvent.TraceId != null)
            {
                properties.Add(("trace.id", ecsEvent.TraceId));
            }

            if (ecsEvent.SpanId != null)
            {
                properties.Add(("span.id", ecsEvent.SpanId));
            }

            return properties;
        }

        private static IEnumerable<(string, object)> GetRequiredProperties(EcsDocument ecsEvent)
        {
            List<(string, object)> propertiesToSort = new();

            AddPropertyFromEcs(propertiesToSort, "error.code", ecsEvent.Error?.Code);
            AddPropertyFromEcs(propertiesToSort, "error.id", ecsEvent.Error?.Id);
            AddPropertyFromEcs(propertiesToSort, "error.message", ecsEvent.Error?.Message);
            AddPropertyFromEcs(propertiesToSort, "error.stack_trace", ecsEvent.Error?.StackTrace);
            AddPropertyFromEcs(propertiesToSort, "error.type", ecsEvent.Error?.Type);
            AddPropertyFromEcs(propertiesToSort, "event.id", ecsEvent.Event?.Id);
            AddPropertyFromEcs(propertiesToSort, "event.created", ecsEvent.Event?.Created);
            AddPropertyFromEcs(propertiesToSort, "event.severity", ecsEvent.Event?.Severity);
            AddPropertyFromEcs(propertiesToSort, "event.timezone", ecsEvent.Event?.Timezone);
            AddPropertyFromEcs(propertiesToSort, "log.logger", ecsEvent.Log?.Logger);
            AddPropertyFromEcs(propertiesToSort, "log.level", ecsEvent.Log?.Level);
            AddPropertyFromEcs(propertiesToSort, "host.hostname", ecsEvent.Host?.Hostname);
            AddPropertyFromEcs(propertiesToSort, "process.executable", ecsEvent.Process?.Executable);
            AddPropertyFromEcs(propertiesToSort, "process.name", ecsEvent.Process?.Name);
            AddPropertyFromEcs(propertiesToSort, "process.pid", ecsEvent.Process?.Pid);
            AddPropertyFromEcs(propertiesToSort, "process.thread.id", ecsEvent.Process?.ThreadId);
            AddPropertyFromEcs(propertiesToSort, "process.thread.name", ecsEvent.Process?.ThreadName);
            AddPropertyFromEcs(propertiesToSort, "service.name", ecsEvent.Service?.Name);
            AddPropertyFromEcs(propertiesToSort, "service.type", ecsEvent.Service?.Type);
            AddPropertyFromEcs(propertiesToSort, "service.version", ecsEvent.Service?.Version);

            AddPropertyFromEcs(propertiesToSort, "client.ip", ecsEvent.Client?.Ip);
            AddPropertyFromEcs(propertiesToSort, "http.request.method", ecsEvent.Http?.RequestMethod);
            AddPropertyFromEcs(propertiesToSort, "url.full", ecsEvent.Url?.Full);
            AddPropertyFromEcs(propertiesToSort, "user_agent.original", ecsEvent.UserAgent?.Original);

            return propertiesToSort;
        }

        private static void AddPropertyFromEcs(List<(string, object)> destination, string propertyName, object? value)
        {
            if (value != null)
            {
                destination.Add((propertyName, value));
            }
        }

        private static IEnumerable<(string, object)> GetAdditionalProperties(LogEvent logEvent)
        {
            foreach (var property in logEvent.Properties)
            {
                if(property.Value is ScalarValue scalarValue && scalarValue.Value != null)
                {
                    yield return (property.Key, scalarValue.Value);
                }
            }
        }
    }
}
