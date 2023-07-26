using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppForeach.Framework.EntityFrameworkCore
{
    internal class CustomSqlServerRetryingExecutionStrategy : SqlServerRetryingExecutionStrategy
    {
        private readonly IEnumerable<ITransactionRetryExceptionHandler> retryExceptionHandlers;

        public CustomSqlServerRetryingExecutionStrategy(ExecutionStrategyDependencies dependencies,
            int maxRetryCount, TimeSpan maxRetryDelay, IEnumerable<ITransactionRetryExceptionHandler> retryExceptionHandlers) 
            : base(dependencies, maxRetryCount, maxRetryDelay, Enumerable.Empty<int>())
        {
            this.retryExceptionHandlers = retryExceptionHandlers;
        }

        protected override bool ShouldRetryOn(Exception exception)
        {
            bool shouldRetry = retryExceptionHandlers.Any(h => h.ShouldRetryTransaction(exception));

            if (!shouldRetry)
            {
                shouldRetry = base.ShouldRetryOn(exception);
            }

            return shouldRetry;
        }
    }
}
