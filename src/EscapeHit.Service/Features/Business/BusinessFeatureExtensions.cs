using AppForeach.Framework;
using AppForeach.Framework.Microsoft.Extensions.DependencyInjection;
using EscapeHit;
using Scrutor;

namespace Microsoft.Extensions.DependencyInjection;

public static class BusinessFeatureExtensions
{
    public static void AddEscapeHitBusiness<TBusinessComponents>(this IServiceCollection services)
        where TBusinessComponents : EscapeHitComponentModule, new()
    {
        services.AddFrameworkModule<TBusinessComponents>();

        //TODO: validation support is not yet implemented
        var validatorMap = new ValidatorMap([]);
        services.AddSingleton<IValidatorMap>(validatorMap);

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
