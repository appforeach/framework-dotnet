using AppForeach.Framework.AutoMapper.Metadata;
using AppForeach.Framework.DependencyInjection;
using AppForeach.Framework.Mapping;

namespace AppForeach.Framework.AutoMapper;
public class AutoMapperFrameworkModule : FrameworkModule
{
    public AutoMapperFrameworkModule()
    {
        Singleton<IMappingMetadataProvider, MappingMetadataProvider>();
        Singleton<IFrameworkMapper, AutoMapperFrameworkMapper>();
    }
}