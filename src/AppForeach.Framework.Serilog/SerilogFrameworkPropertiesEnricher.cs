using Serilog.Core;
using Serilog.Events;

namespace AppForeach.Framework.Serilog
{
    internal class SerilogFrameworkPropertiesEnricher : ILogEventEnricher
    {
        private readonly Dictionary<string, object> properties;

        public SerilogFrameworkPropertiesEnricher(Dictionary<string, object> properties)
        {
            this.properties = properties;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            foreach (var property in properties)
            {
                logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty(property.Key, property.Value));
            }
        }
    }
}
