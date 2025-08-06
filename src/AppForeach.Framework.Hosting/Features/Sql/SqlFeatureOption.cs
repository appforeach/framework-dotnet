using AppForeach.Framework.Hosting.Startup;
using Microsoft.EntityFrameworkCore;
using System;

namespace AppForeach.Framework.Hosting.Features.Sql
{
    public abstract class SqlFeatureOption<TDbContext> : ISqlFeatureOption, IApplicationFeatureOption
        where TDbContext : DbContext
    {
        public abstract IApplicationFeatureInstaller Installer { get; }

        public string? ConnectionStringName {  get; set; }

        public Action<IApplicationStartupOptionsConfigurator>? MigrationStartupConfigureAction { get; set; }
    }
}
