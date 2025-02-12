using System.Collections.Generic;

namespace AppForeach.Framework.Logging
{
    public interface IFrameworkLogger
    {
        void Log(FrameworkLogEventId eventId, FrameworkLogLevel logLevel, string message, Dictionary<string, object> properties = null);
    }
}
