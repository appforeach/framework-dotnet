using Microsoft.EntityFrameworkCore;

namespace AppForeach.Framework.Hosting.Features.Sql
{
    public class SqlFeatureOption : ISqlFeatureOption
    {
    }
    public class SqlFeatureOption<TDbContext> : ISqlFeatureOption
        where TDbContext : DbContext
    {
        public IApplicationFeatureInstaller Installer => new SqlFeatureInstaller<TDbContext>(this);
    }
}
