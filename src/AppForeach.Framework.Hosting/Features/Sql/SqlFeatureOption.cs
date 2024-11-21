using Microsoft.EntityFrameworkCore;

namespace AppForeach.Framework.Hosting.Features.Sql
{
    public abstract class SqlFeatureOption<TDbContext> : ISqlFeatureOption, IApplicationFeatureOption
        where TDbContext : DbContext
    {
        public abstract IApplicationFeatureInstaller Installer { get; }

        public string? ConnectionStringName {  get; set; }
    }
}
