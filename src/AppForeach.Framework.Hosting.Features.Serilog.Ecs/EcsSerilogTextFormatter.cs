using AppForeach.Framework.Hosting.Features.Logging;
using Elastic.CommonSchema;
using Elastic.CommonSchema.Serilog;
using Serilog.Events;
using Serilog.Formatting;

namespace AppForeach.Framework.Hosting.Features.Serilog.Ecs
{
    public class EcsSerilogTextFormatter : ITextFormatter
    {
        private readonly EcsTextFormatterConfiguration ecsConfiguration = new();
        
        private static readonly HashSet<string> ecsTopPathes =
            ["agent", "as", "client", "cloud", "code_signature", "container", "data_stream",
            "destination", "device", "dll", "dns", "elf", "email", "error",
            "event", "faas", "file", "geo", "group", "hash", "host",
            "http", "log", "macho", "network", "interface", "orchestrator", "organization",
            "os", "package", "pe", "process", "registry", "related", "risk", "rule", "server",
            "service", "source", "threat", "tls", "url", "user", "user_agent", "vlan", "volume",
            "vulnerability", "x509"];

        private static readonly HashSet<string> ecsNotExpandableProperties =
            ["ecs.version", "span.id", "trace.id", "transaction.id"];

        private static readonly HashSet<string> requiredProperties =
            ["span.id", "trace.id", "transaction.id", "event.id", "event.created", "event.severity", "event.timezone", "log.logger", "log.level", 
            "host.hostname", "process.executable", "process.name", "process.pid", "process.thread.id", 
            "process.thread.name", "service.name", "service.type", "service.version"];

        private static readonly JsonPropertyConverter jsonConverter = new EcsJsonPropertyConverter(ecsNotExpandableProperties);

        public void Format(LogEvent logEvent, TextWriter output)
        {
            var ecsEvent = LogEventConverter.ConvertToEcs(logEvent, ecsConfiguration);

            List<(string, object)> properties = new();
            List<(string, object)> propertiesToSort = new();

            AddHeaderProperties(logEvent, ecsEvent, properties);

            AddRequiredProperties(ecsEvent, propertiesToSort);

            AddAdditionalProperties(logEvent, propertiesToSort);

            properties.AddRange(propertiesToSort.OrderBy(i => i.Item1));

            output.WriteLine(jsonConverter.GetJson(properties));
        }

        private static void AddHeaderProperties(LogEvent logEvent, EcsDocument ecsEvent, List<(string, object)> properties)
        {
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
        }

        private void AddRequiredProperties(EcsDocument ecsEvent, List<(string, object)> propertiesToSort)
        {
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
        }

        private void AddPropertyFromEcs(List<(string, object)> destination, string propertyName, object? value)
        {
            if (value != null)
            {
                destination.Add((propertyName, value));
            }
        }

        private static void AddAdditionalProperties(LogEvent logEvent, List<(string, object)> propertiesToSort)
        {
            foreach (var property in logEvent.Properties)
            {
                if (requiredProperties.Contains(property.Key))
                {
                    continue;
                }

                string? ecsPropertyName = null;

                int topPropertyEndIndex = property.Key.IndexOf('.');

                if (topPropertyEndIndex > 0)
                {
                    string topProperty = property.Key.Substring(0, topPropertyEndIndex);

                    if (ecsTopPathes.Contains(topProperty))
                    {
                        ecsPropertyName = property.Key;
                    }
                }

                if (ecsPropertyName == null)
                {
                    ecsPropertyName = "metadata." + property.Key;
                }

                if(property.Value is ScalarValue scalarValue && scalarValue.Value != null)
                {
                    propertiesToSort.Add((ecsPropertyName, scalarValue.Value));
                }
            }
        }
    }
}
