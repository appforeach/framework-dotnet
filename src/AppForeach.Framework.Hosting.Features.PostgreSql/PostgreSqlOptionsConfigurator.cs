using AppForeach.Framework.Hosting.Features.Sql;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace AppForeach.Framework.Hosting.Features.PostgreSql;

public class PostgreSqlOptionsConfigurator<TDbContext> 
    (
        PostgreSqlFeatureOption<TDbContext> option
    )
    : SqlOptionsConfigurator<TDbContext>(option), IPostgreSqlOptionsConfigurator
    where TDbContext : DbContext
{
    public void ExecutionDbContextOptions(Action<NpgsqlDbContextOptionsBuilder> options)
    {
        option.ExecutionDbContextOptions = options;
    }

    public void MigrationDbContextOptions(Action<NpgsqlDbContextOptionsBuilder> options)
    {
        option.MigrationDbContextOptions = options;
    }

    public void FrameworkMigrationDbContextOptions(Action<NpgsqlDbContextOptionsBuilder> optionsAction)
    {
        option.FrameworkMigrationDbContextOptions = optionsAction;
    }
}
