using System.Collections.Generic;

namespace AppForeach.Framework.Logging
{
    public class DefaultLoggingPropertyMap : ILoggingPropertyMap
    {
        public Dictionary<string, object> MapProperties(Dictionary<string, object> properties)
            => properties;
    }
}
