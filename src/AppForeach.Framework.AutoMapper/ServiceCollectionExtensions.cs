using AppForeach.Framework.AutoMapper.Metadata;
using AppForeach.Framework.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace AppForeach.Framework.AutoMapper;
public static class ServiceCollectionExtensions
{
    public static void AddAutoMapper<T>(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(T));
        services.AddSingleton<IMappingMetadataProvider, MappingMetadataProvider>();
    }
}
