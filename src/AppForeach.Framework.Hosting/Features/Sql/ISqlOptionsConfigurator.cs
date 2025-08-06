using AppForeach.Framework.Hosting.Startup;
using System;

namespace AppForeach.Framework.Hosting.Features.Sql
{
    public interface ISqlOptionsConfigurator
    {
        void ConnectionStringName(string connectionStringName);
        void MigrationStartup(Action<IApplicationStartupOptionsConfigurator> configureStartupAction);
    }
}
