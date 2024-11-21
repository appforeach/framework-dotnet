using System;
using System.Collections.Generic;
using System.Text;

namespace AppForeach.Framework.Logging
{
    public class DefaultLoggingCorrelationProvider : ILoggingCorrelationProvider
    {
        public LoggingCorrelationInfo CorrelationInfo => new LoggingCorrelationInfo
        {
            TraceId = string.Empty,
            TransactionId = string.Empty,
        };
    }
}
