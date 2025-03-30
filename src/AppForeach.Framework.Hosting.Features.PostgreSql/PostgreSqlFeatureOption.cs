using AppForeach.Framework.Hosting.Features.Sql;
using Microsoft.EntityFrameworkCore;

namespace AppForeach.Framework.Hosting.Features.PostgreSql;

public class PostgreSqlFeatureOption<TDbContext> : SqlFeatureOption<TDbContext>, ISqlFeatureOption, IApplicationFeatureOption
    where TDbContext : DbContext
{
    public override IApplicationFeatureInstaller Installer => new PostgreSqlFeatureInstaller<TDbContext>(this);
}
