using System;
using System.Collections.Generic;
using System.Text;

namespace AppForeach.Framework.Logging
{
    public interface ILoggingCorrelationProvider
    {
        LoggingCorrelationInfo CorrelationInfo { get; }
    }
}
