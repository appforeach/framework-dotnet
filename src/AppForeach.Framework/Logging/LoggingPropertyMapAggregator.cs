using System.Collections.Generic;

namespace AppForeach.Framework.Logging
{
    internal class LoggingPropertyMapAggregator : ILoggingPropertyMapAggregator
    {
        private readonly IEnumerable<ILoggingPropertyMap> propertyMaps;

        public LoggingPropertyMapAggregator(IEnumerable<ILoggingPropertyMap> propertyMaps)
        {
            this.propertyMaps = propertyMaps;
        }

        public IEnumerable<KeyValuePair<string, object>> MapProperties(IEnumerable<KeyValuePair<string, object>> properties)
        {
            var currentProperties = properties;

            foreach (var propertyMap in propertyMaps)
            {
                currentProperties = propertyMap.MapProperties(currentProperties);
            }

            return currentProperties;
        }
    }
}
