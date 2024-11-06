using AppForeach.Framework.Hosting.Features.Sql;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SqlFeatureExtensions
    {
        public static void AddApplicationSql<TDbContext>(this IServiceCollection services)
             where TDbContext : DbContext
        {
            var options = new SqlFeatureOption<TDbContext>();
            services.AddSingleton(options);
        }
    }
}
