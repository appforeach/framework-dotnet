﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AppForeach.Framework.EntityFrameworkCore
{
    public class CustomSqlServerRetryingExecutionStrategy : SqlServerRetryingExecutionStrategy
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
