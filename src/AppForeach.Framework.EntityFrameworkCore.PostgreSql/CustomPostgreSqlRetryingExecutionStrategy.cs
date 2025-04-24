using Microsoft.EntityFrameworkCore.Storage;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace AppForeach.Framework.EntityFrameworkCore.PostgreSql;

internal class CustomPostgreSqlRetryingExecutionStrategy : NpgsqlRetryingExecutionStrategy
{
    private readonly IEnumerable<ITransactionRetryExceptionHandler> retryExceptionHandlers;

    public CustomPostgreSqlRetryingExecutionStrategy(ExecutionStrategyDependencies dependencies,
        int maxRetryCount, TimeSpan maxRetryDelay, IEnumerable<ITransactionRetryExceptionHandler> retryExceptionHandlers)
        : base(dependencies, maxRetryCount, maxRetryDelay, [])
    {
        this.retryExceptionHandlers = retryExceptionHandlers;
    }

    protected override bool ShouldRetryOn(Exception? exception)
    {
        bool shouldRetry = retryExceptionHandlers.Any(h => h.ShouldRetryTransaction(exception));

        if (!shouldRetry)
        {
            shouldRetry = base.ShouldRetryOn(exception);
        }

        return shouldRetry;
    }
}
