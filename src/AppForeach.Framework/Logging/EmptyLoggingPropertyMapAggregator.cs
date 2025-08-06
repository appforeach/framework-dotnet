using System.Collections.Generic;

namespace AppForeach.Framework.Logging
{
    public class EmptyLoggingPropertyMapAggregator : ILoggingPropertyMapAggregator
    {
        public IEnumerable<KeyValuePair<string, object>> MapProperties(IEnumerable<KeyValuePair<string, object>> properties)
            => properties;
    }
}
