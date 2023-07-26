using System;
using System.Data;

namespace AppForeach.Framework.EntityFrameworkCore
{
    public static class TransactionOperationBuilderExtensions
    {
        public static IOperationBuilder TransactionIsolationLevel(this IOperationBuilder builder, IsolationLevel isolationLevel)
        {
            var facet = new TransactionIsolationLevelFacet
            {
                IsolationLevel = isolationLevel
            };
            builder.Configuration.Set(facet);

            return builder;
        }

        public static IOperationBuilder TransactionRetry(this IOperationBuilder builder, bool retry)
        {
            var transactionRetryFacet = new TransactionRetryFacet
            {
                Retry = retry
            };
            builder.Configuration.Set(transactionRetryFacet);

            builder.OperationCreateScopeForExecution(true);

            return builder;
        }

        public static IOperationBuilder TransactionRetryCount(this IOperationBuilder builder, int retry)
        {
            var facet = new TransactionRetryCountFacet
            {
                RetryCount = retry
            };
            builder.Configuration.Set(facet);

            return builder;
        }

        public static IOperationBuilder TransactionMaxRetryDelay(this IOperationBuilder builder, TimeSpan maxRetryDelay)
        {
            var facet = new TransactionMaxRetryDelayFacet
            {
                MaxRetryDelay = maxRetryDelay
            };
            builder.Configuration.Set(facet);

            return builder;
        }
    }
}
