using AppForeach.Framework.Hosting.Features.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AppForeach.Framework.Hosting.Features.SqlServer;

public class SqlServerFeatureOption<TDbContext> : SqlFeatureOption<TDbContext>, ISqlFeatureOption, IApplicationFeatureOption
    where TDbContext : DbContext
{
    public override IApplicationFeatureInstaller Installer => new SqlServerFeatureInstaller<TDbContext>(this);

    public Action<SqlServerDbContextOptionsBuilder>? ExecutionDbContextOptions { get; set; }

    public Action<SqlServerDbContextOptionsBuilder>? MigrationDbContextOptions { get; set; }

    public Action<SqlServerDbContextOptionsBuilder>? FrameworkMigrationDbContextOptions { get; set; }
}
