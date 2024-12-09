using Microsoft.Extensions.DependencyInjection;

namespace AppForeach.Framework.Automapper;
public static class ServiceCollectionExtensions
{
    public static void AddAutoMapper<T>(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(T));
        services.AddSingleton<IMapper, MapperDecorator>();
    }
}
