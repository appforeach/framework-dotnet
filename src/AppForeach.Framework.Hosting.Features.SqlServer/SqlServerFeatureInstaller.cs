using AppForeach.Framework.EntityFrameworkCore.SqlServer;
using AppForeach.Framework.Hosting.Features.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AppForeach.Framework.Microsoft.Extensions.DependencyInjection;
using AppForeach.Framework.EntityFrameworkCore;

namespace AppForeach.Framework.Hosting.Features.Sql
{
    public class SqlServerFeatureInstaller<TDbContext> : SqlFeatureInstaller<TDbContext>
        where TDbContext : DbContext
    {
        private readonly SqlServerFeatureOption<TDbContext> option;

        public SqlServerFeatureInstaller(SqlServerFeatureOption<TDbContext> option) : base(option)
        {
            this.option = option;
        }

        public override void SetUpServices(IApplicationFeatureInstallContext installContext, IServiceCollection services)
        {
            base.SetUpServices(installContext, services);


            services.AddFrameworkModule<SqlEntityFrameworkComponents>();

            services.AddApplicationStartup<SqlServerMigrationStartup<TDbContext>>(option.MigrationStartupConfigureAction);
            services.Configure<SqlServerMigrationOptions<TDbContext>>(opt => opt.ConnectionString = connectionString);

            services.AddApplicationStartup<SqlServerMigrationStartup<FrameworkDbContext>>(option.MigrationStartupConfigureAction);
            services.Configure<SqlServerMigrationOptions<FrameworkDbContext>>(opt =>
            {
                opt.ConnectionString = connectionString;
                opt.DbContextOptions = opt => opt
                    .MigrationsHistoryTable("__FrameworkEFMigrationsHistory", "framework")
                    .MigrationsAssembly("AppForeach.Framework.EntityFrameworkCore.SqlServer");
            });
        }
    }
}
