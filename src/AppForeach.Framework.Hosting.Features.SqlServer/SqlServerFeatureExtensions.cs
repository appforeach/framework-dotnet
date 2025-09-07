using AppForeach.Framework.Hosting.Features;
using AppForeach.Framework.Hosting.Features.SqlServer;
using AppForeach.Framework.Hosting.Features.Tag;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SqlServerFeatureExtensions
    {
        public static void AddApplicationSqlServer<TDbContext>(this IServiceCollection services, Action<ISqlServerOptionsConfigurator>? configureAction = null)
             where TDbContext : DbContext
        {
            var options = new SqlServerFeatureOption<TDbContext>();

            var configurator = new SqlServerOptionsConfigurator<TDbContext>(options);
            configureAction?.Invoke(configurator);

            services.AddSingleton(options);

            services.AddApplicationFeatureTag(FrameworkFeatureTags.Sql);
        }
    }
}
