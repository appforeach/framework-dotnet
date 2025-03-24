using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace AppForeach.Framework.EntityFrameworkCore.PostgreSql;

public class PostgreSqlDbOptionsConfigurator : IDbOptionsConfigurator
{
    private readonly IEnumerable<ITransactionRetryExceptionHandler> retryExceptionHandlers;

    public PostgreSqlDbOptionsConfigurator(IEnumerable<ITransactionRetryExceptionHandler> retryExceptionHandlers)
    {
        this.retryExceptionHandlers = retryExceptionHandlers;
    }

    public void SetConnection<TDbContext>(DbContextOptionsBuilder<TDbContext> optionsBuilder, DbConnection connection) where TDbContext : DbContext
    {
        optionsBuilder.UseNpgsql(connection);
    }

    public void SetConnectionString<TDbContext>(DbContextOptionsBuilder<TDbContext> optionsBuilder, string connectionString, TransactionRetrySettings? retrySettings = null)
        where TDbContext : DbContext
    {
        optionsBuilder.UseNpgsql(connectionString, sqlOpt =>
        {
            if (retrySettings?.Retry ?? false)
            {
                sqlOpt.ExecutionStrategy(dp => new CustomPostgreSqlRetryingExecutionStrategy(dp, retrySettings.RetryCount, retrySettings.RetryDelay, retryExceptionHandlers));
            }
        });
    }
}
