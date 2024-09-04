using AppForeach.Framework.Hosting.Features.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SqlServerFeatureExtensions
    {
        public static void AddApplicationSqlServer<TDbContext>(this IServiceCollection services)
             where TDbContext : DbContext
        {
            var options = new SqlServerFeatureOption<TDbContext>();
            services.AddSingleton(options);
        }
    }
}
