using AppForeach.Framework.Microsoft.Extensions.DependencyInjection;
using AppForeach.Framework.Validation;
using AppForeach.Framework.Automapper;
using EscapeHit;
using Scrutor;

namespace Microsoft.Extensions.DependencyInjection;

public static class BusinessFeatureExtensions
{
    public static void AddEscapeHitBusiness<TBusinessComponents>(this IServiceCollection services)
        where TBusinessComponents : EscapeHitComponentModule, new()
    {
        services.AddFrameworkModule<TBusinessComponents>();

        services.AddAutoMapper<TBusinessComponents>();

        //TODO: use internal scanner
        services.Scan(scan => scan
          .FromAssemblies(typeof(TBusinessComponents).Assembly)
          .AddClasses(filter => filter.InNamespaces("EscapeHit"), true)
          .UsingRegistrationStrategy(RegistrationStrategy.Append)
          .AsImplementedInterfaces()
          .WithLifetime(ServiceLifetime.Transient)
          );
    }
}
