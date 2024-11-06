using AppForeach.Framework.Hosting.Features.Sql;
using Microsoft.EntityFrameworkCore;
using Scrutor;

namespace Microsoft.Extensions.DependencyInjection;

public static class SqlFeatureExtensions
{
    public static void AddEscapeHitSql<TDbContext>(this IServiceCollection services)
         where TDbContext : DbContext
    {
        var options = new SqlFeatureOption<TDbContext>();
        services.AddSingleton(options);

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
