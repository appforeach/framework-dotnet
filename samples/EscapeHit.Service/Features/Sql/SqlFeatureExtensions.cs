using Microsoft.EntityFrameworkCore;
using Scrutor;

namespace Microsoft.Extensions.DependencyInjection;

public static class SqlFeatureExtensions
{
    public static void AddEscapeHitSql<TDbContext>(this IServiceCollection services)
         where TDbContext : DbContext
    {
        services.AddApplicationSqlServer<TDbContext>();

        //TODO: use internal scanner
        services.Scan(scan => scan
            .FromAssemblies(typeof(TDbContext).Assembly)
            .AddClasses(filter => filter.Where(t => t.Name.EndsWith("Repository")), true)
            .UsingRegistrationStrategy(RegistrationStrategy.Append)
            .AsImplementedInterfaces()
            .WithLifetime(ServiceLifetime.Transient)
            );
    }
}
