using AppForeach.Framework.Hosting.Features.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AppForeach.Framework.Hosting.Features.SqlServer
{
    public class SqlServerOptionsConfigurator<TDbContext>
        (
            SqlServerFeatureOption<TDbContext> option
        )
        : SqlOptionsConfigurator<TDbContext>(option), ISqlServerOptionsConfigurator
        where TDbContext : DbContext
    {
        public void ExecutionDbContextOptions(Action<SqlServerDbContextOptionsBuilder> options)
        {
            option.ExecutionDbContextOptions = options;
        }

        public void MigrationDbContextOptions(Action<SqlServerDbContextOptionsBuilder> options)
        {
            option.MigrationDbContextOptions = options;
        }

        public void FrameworkMigrationDbContextOptions(Action<SqlServerDbContextOptionsBuilder> optionsAction)
        {
            option.FrameworkMigrationDbContextOptions = optionsAction;
        }
    }
}
