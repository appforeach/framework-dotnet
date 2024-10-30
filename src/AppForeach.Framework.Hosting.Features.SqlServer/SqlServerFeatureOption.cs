using AppForeach.Framework.Hosting.Features.Sql;
using Microsoft.EntityFrameworkCore;

namespace AppForeach.Framework.Hosting.Features.SqlServer
{
    public class SqlServerFeatureOption<TDbContext> : SqlFeatureOption<TDbContext>, ISqlFeatureOption, IApplicationFeatureOption
        where TDbContext : DbContext
    {
        public override IApplicationFeatureInstaller Installer => new SqlServerFeatureInstaller<TDbContext>(this);
    }
}
