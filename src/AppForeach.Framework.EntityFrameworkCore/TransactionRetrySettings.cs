using System;

namespace AppForeach.Framework.EntityFrameworkCore
{
    public class TransactionRetrySettings
    {
        public bool Retry { get; set; }

        public int RetryCount { get; set; }

        public TimeSpan RetryDelay { get; set; }    
    }
}
