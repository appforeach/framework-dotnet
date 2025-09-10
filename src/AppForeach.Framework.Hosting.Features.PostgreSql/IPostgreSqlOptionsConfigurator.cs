using AppForeach.Framework.Hosting.Features.Sql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace AppForeach.Framework.Hosting.Features.PostgreSql;

public interface IPostgreSqlOptionsConfigurator : ISqlOptionsConfigurator
{
    void ExecutionDbContextOptions(Action<NpgsqlDbContextOptionsBuilder> options);

    void MigrationDbContextOptions(Action<NpgsqlDbContextOptionsBuilder> options);

    void FrameworkMigrationDbContextOptions(Action<NpgsqlDbContextOptionsBuilder> options);
}
