using System.Collections.Generic;

namespace AppForeach.Framework.Logging
{
    public interface ILoggingPropertyMap
    {
        Dictionary<string, object> MapProperties(Dictionary<string, object> properties);
    }
}
