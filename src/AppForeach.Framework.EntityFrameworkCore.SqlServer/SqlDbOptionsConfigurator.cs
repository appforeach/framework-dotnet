using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace AppForeach.Framework.EntityFrameworkCore.SqlServer
{
    public class SqlDbOptionsConfigurator : IDbOptionsConfigurator
    {
        private readonly IEnumerable<ITransactionRetryExceptionHandler> retryExceptionHandlers;

        public SqlDbOptionsConfigurator(IEnumerable<ITransactionRetryExceptionHandler> retryExceptionHandlers)
        {
            this.retryExceptionHandlers = retryExceptionHandlers;
        }

        public void SetConnection<TDbContext>(DbContextOptionsBuilder<TDbContext> optionsBuilder, DbConnection connection) where TDbContext : DbContext
        {
            optionsBuilder.UseSqlServer(connection);
        }

        public void SetConnectionString<TDbContext>(DbContextOptionsBuilder<TDbContext> optionsBuilder, string connectionString, TransactionRetrySettings? retrySettings = null) 
            where TDbContext : DbContext
        {
            optionsBuilder.UseSqlServer(connectionString, sqlOpt =>
            {
                if(retrySettings?.Retry ?? false)
                {
                    sqlOpt.ExecutionStrategy(dp => new CustomSqlServerRetryingExecutionStrategy(dp, retrySettings.RetryCount, retrySettings.RetryDelay, retryExceptionHandlers));
                }
            });
        }
    }
}
