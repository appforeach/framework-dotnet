using System;

namespace AppForeach.Framework.EntityFrameworkCore
{
    internal class TransactionMaxRetryDelayFacet
    {
        public TimeSpan MaxRetryDelay { get; set; }
    }
}
