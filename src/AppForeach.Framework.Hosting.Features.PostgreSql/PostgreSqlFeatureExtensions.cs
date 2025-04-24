using AppForeach.Framework.Hosting.Features.PostgreSql;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class PostgreSqlFeatureExtensions
    {
        public static void AddApplicationPostgreSql<TDbContext>(this IServiceCollection services)
             where TDbContext : DbContext
        {
            var option = new PostgreSqlFeatureOption<TDbContext>();
            services.AddSingleton(option);
        }
    }
}
