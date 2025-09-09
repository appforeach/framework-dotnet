using AppForeach.Framework.Hosting.Features.Sql;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace AppForeach.Framework.Hosting.Features.PostgreSql;

public class PostgreSqlFeatureOption<TDbContext> : SqlFeatureOption<TDbContext>, ISqlFeatureOption, IApplicationFeatureOption
    where TDbContext : DbContext
{
    public override IApplicationFeatureInstaller Installer => new PostgreSqlFeatureInstaller<TDbContext>(this);

    public Action<NpgsqlDbContextOptionsBuilder>? ExecutionDbContextOptions { get; set; }

    public Action<NpgsqlDbContextOptionsBuilder>? MigrationDbContextOptions { get; set; }

    public Action<NpgsqlDbContextOptionsBuilder>? FrameworkMigrationDbContextOptions { get; set; }

}
