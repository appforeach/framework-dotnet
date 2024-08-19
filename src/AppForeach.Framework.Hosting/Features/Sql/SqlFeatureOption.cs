using Microsoft.EntityFrameworkCore;

namespace AppForeach.Framework.Hosting.Features.Sql
{
    public class SqlFeatureOption<TDbContext> : ISqlFeatureOption, IApplicationFeatureOption
        where TDbContext : DbContext
    {
        public IApplicationFeatureInstaller Installer => new SqlFeatureInstaller<TDbContext>(this);

        public string? ConnectionStringName {  get; set; }
    }
}
