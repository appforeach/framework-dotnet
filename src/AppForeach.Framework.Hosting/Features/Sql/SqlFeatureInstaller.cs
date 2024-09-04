using AppForeach.Framework.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AppForeach.Framework.Microsoft.Extensions.DependencyInjection;

namespace AppForeach.Framework.Hosting.Features.Sql
{
    public class SqlFeatureInstaller<TDbContext> : IApplicationFeatureInstaller
        where TDbContext : DbContext
    {
        private readonly SqlFeatureOption<TDbContext> option;
        protected string? connectionString;

        public SqlFeatureInstaller(SqlFeatureOption<TDbContext> option)
        {
            this.option = option;
        }

        public virtual void SetUpServices(IApplicationFeatureInstallContext installContext, IServiceCollection services)
        {
            string connectionStringName = "Sql";
            connectionString = installContext.Configuration.GetConnectionString(connectionStringName)
                ?? throw new FrameworkException($"Connection string '{connectionStringName}' not found.");

            services.AddSingleton<IConnectionStringProvider>(new ConnectionStringProvider(connectionString));

            services.AddFrameworkModule<EntityFrameworkComponents>();

            services.AddScoped(sp =>
            {
                var activator = sp.GetRequiredService<IDbContextActivator>();
                return activator.Activate<TDbContext>();
            });
        }
    }
}
