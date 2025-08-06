using System.Collections.Generic;

namespace AppForeach.Framework.Logging
{
    public class DefaultEmptyFrameworkLogger : IFrameworkLogger
    {
        public void Log(FrameworkLogEventId eventId, FrameworkLogLevel loglLevel, string message, Dictionary<string, object> properties = null)
        {
            // empty default implementation
        }
    }
}
