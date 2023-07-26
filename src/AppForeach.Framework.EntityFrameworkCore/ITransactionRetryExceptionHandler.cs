using System;

namespace AppForeach.Framework.EntityFrameworkCore
{
    public interface ITransactionRetryExceptionHandler
    {
        bool ShouldRetryTransaction(Exception exception);
    }
}
