using AppForeach.Framework.Logging;
using Serilog.Core;
using Serilog.Events;

namespace AppForeach.Framework.Serilog
{
    public class SerilogFrameworkAggregatedPropertyProvider : ILogEventEnricher
    {
        private readonly ILoggingPropertyAggregator propertyAggregator;

        public SerilogFrameworkAggregatedPropertyProvider(ILoggingPropertyAggregator propertyAggregator)
        {
            this.propertyAggregator = propertyAggregator;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if(propertyAggregator?.Properties == null)
            {
                return;
            }

            foreach (var property in propertyAggregator.Properties)
            {
                logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty(property.Key, property.Value));
            }
        }
    }
}
