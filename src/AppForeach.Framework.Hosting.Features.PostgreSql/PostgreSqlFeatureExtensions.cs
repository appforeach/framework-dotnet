using AppForeach.Framework.Hosting.Features;
using AppForeach.Framework.Hosting.Features.PostgreSql;
using AppForeach.Framework.Hosting.Features.Tag;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class PostgreSqlFeatureExtensions
    {
        public static void AddApplicationPostgreSql<TDbContext>(this IServiceCollection services, Action<IPostgreSqlOptionsConfigurator>? configureAction = null)
             where TDbContext : DbContext
        {
            var option = new PostgreSqlFeatureOption<TDbContext>();

            var configurator = new PostgreSqlOptionsConfigurator<TDbContext>(option);
            configureAction?.Invoke(configurator);

            services.AddSingleton(option);

            services.AddApplicationFeatureTag(FrameworkFeatureTags.Sql);
        }
    }
}
