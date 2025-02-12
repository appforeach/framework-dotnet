using System.Collections.Generic;

namespace AppForeach.Framework.Logging
{
    public interface ILoggingPropertyProvider
    {
        Dictionary<string, object> Properties { get; }
    }
}
