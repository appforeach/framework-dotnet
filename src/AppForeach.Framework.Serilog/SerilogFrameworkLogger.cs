using AppForeach.Framework.Logging;
using Serilog;
using Serilog.Events;

namespace AppForeach.Framework.Serilog
{
    public class SerilogFrameworkLogger : IFrameworkLogger
    {
        private readonly ILogger logger;
        private readonly ILoggingPropertyMap propertyMap;

        public SerilogFrameworkLogger(ILogger logger, ILoggingPropertyMap propertyMap)
        {
            this.logger = logger;
            this.propertyMap = propertyMap;
        }

        public void Log(FrameworkLogEventId eventId, FrameworkLogLevel logLevel, string message, Dictionary<string, object>?properties = null)
        {
            properties = properties ?? new Dictionary<string, object>();

            properties.Add("EventId", eventId.Id);
            properties.Add("EventName", eventId.Name);

            properties = propertyMap.MapProperties(properties);

            var enrichedLogger = logger.ForContext(new SerilogFrameworkPropertiesEnricher(properties));

            enrichedLogger.Write(MapLogLevel(logLevel), message);
        }

        private static LogEventLevel MapLogLevel(FrameworkLogLevel logLevel)
        {
            switch (logLevel)
            {
                case FrameworkLogLevel.Trace:
                    return LogEventLevel.Verbose;
                case FrameworkLogLevel.Debug:
                    return LogEventLevel.Debug;
                case FrameworkLogLevel.Information:
                    return LogEventLevel.Information;
                case FrameworkLogLevel.Warning:
                    return LogEventLevel.Warning;
                case FrameworkLogLevel.Error:
                    return LogEventLevel.Error;
                default:
                    throw new FrameworkException("Unsupported log level " + logLevel);
            }
        }
    }
}
