using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace AppForeach.Framework.EntityFrameworkCore
{
    public interface IDbOptionsConfigurator
    {
        void SetConnection<TDbContext>(DbContextOptionsBuilder<TDbContext> optionsBuilder, DbConnection connection)
            where TDbContext : DbContext;

        void SetConnectionString<TDbContext>(DbContextOptionsBuilder<TDbContext> optionsBuilder, string connectionString, TransactionRetrySettings? retrySettings = null)
            where TDbContext : DbContext;
    }
}
