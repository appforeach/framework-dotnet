using System.Collections.Generic;

namespace AppForeach.Framework.Logging
{
    public class LoggingPropertyAggregator : ILoggingPropertyAggregator
    {
        private readonly IEnumerable<ILoggingPropertyProvider> propertyProviders;

        public LoggingPropertyAggregator(IEnumerable<ILoggingPropertyProvider> propertyProviders)
        {
            this.propertyProviders = propertyProviders;
        }

        public Dictionary<string, object> Properties
        { 
            get
            {
                Dictionary<string, object> combined = new Dictionary<string, object>();

                foreach (var provider in propertyProviders)
                {
                    if(provider.Properties == null)
                    {
                        continue;
                    }

                    foreach(var kvp in provider.Properties)
                    {
                        combined[kvp.Key] = kvp.Value;
                    }
                }

                return combined;
            }
        }
    }
}
