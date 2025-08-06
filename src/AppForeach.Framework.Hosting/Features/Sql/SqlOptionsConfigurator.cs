using AppForeach.Framework.Hosting.Startup;
using Microsoft.EntityFrameworkCore;
using System;

namespace AppForeach.Framework.Hosting.Features.Sql
{
    public class SqlOptionsConfigurator<TDbContext> 
        (
            SqlFeatureOption<TDbContext> option
        ) : ISqlOptionsConfigurator
        where TDbContext : DbContext
    {
        public void ConnectionStringName(string connectionStringName)
        {
            option.ConnectionStringName = connectionStringName;
        }

        public void MigrationStartup(Action<IApplicationStartupOptionsConfigurator> configureStartupAction)
        {
            option.MigrationStartupConfigureAction = configureStartupAction;
        }
    }
}
