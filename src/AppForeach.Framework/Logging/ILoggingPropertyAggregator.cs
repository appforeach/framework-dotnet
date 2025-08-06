using System.Collections.Generic;

namespace AppForeach.Framework.Logging
{
    public interface ILoggingPropertyAggregator
    {
        Dictionary<string, object> Properties { get; }
    }
}
