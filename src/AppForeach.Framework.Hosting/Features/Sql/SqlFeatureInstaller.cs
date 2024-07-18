using AppForeach.Framework.EntityFrameworkCore;
using AppForeach.Framework.Hosting.Startup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AppForeach.Framework.Microsoft.Extensions.DependencyInjection;

namespace AppForeach.Framework.Hosting.Features.Sql
{
    internal class SqlFeatureInstaller<TDbContext> : IApplicationFeatureInstaller
        where TDbContext : DbContext
    {
        private readonly SqlFeatureOption<TDbContext> option;

        public SqlFeatureInstaller(SqlFeatureOption<TDbContext> option)
        {
            this.option = option;
        }

        public void SetUpServices(IApplicationFeatureInstallContext installContext, IServiceCollection services)
        {
            string connectionStringName = "DefaultConnection";
            string connectionString = installContext.Configuration.GetConnectionString(connectionStringName)
                ?? throw new FrameworkException($"Connection string '{connectionStringName}' not found.");

            services.AddSingleton<IConnectionStringProvider>(new ConnectionStringProvider(connectionString));

            services.AddApplicationStartup<SqlMigrationStartup<TDbContext>>();
            services.Configure<SqlMigrationOptions<TDbContext>>(opt => opt.ConnectionString = connectionString);

            services.AddFrameworkModule<EntityFrameworkComponents>();

            services.AddScoped(sp =>
            {
                var activator = sp.GetRequiredService<IDbContextActivator>();
                return activator.Activate<TDbContext>();
            });

            services.AddApplicationStartup<SqlMigrationStartup<FrameworkDbContext>>();
            services.Configure<SqlMigrationOptions<FrameworkDbContext>>(opt =>
            {
                opt.ConnectionString = connectionString;
                opt.DbContextOptions = opt => opt.MigrationsHistoryTable("__FrameworkEFMigrationsHistory", "framework");
            });
        }
    }
}
