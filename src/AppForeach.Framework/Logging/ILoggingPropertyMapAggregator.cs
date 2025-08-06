using System.Collections.Generic;

namespace AppForeach.Framework.Logging
{
    public interface ILoggingPropertyMapAggregator
    {
        IEnumerable<KeyValuePair<string, object>> MapProperties(IEnumerable<KeyValuePair<string, object>> properties);
    }
}
