using System.Collections.Generic;

namespace AppForeach.Framework.Logging
{
    public interface ILoggingPropertyMap
    {
        IEnumerable<KeyValuePair<string, object>> MapProperties(IEnumerable<KeyValuePair<string, object>> properties);
    }
}
