using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AppForeach.Framework.Hosting.Features.Sql
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
