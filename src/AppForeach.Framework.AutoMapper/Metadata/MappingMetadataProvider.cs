using AppForeach.Framework.Mapping;
using AutoMapper;
using AutoMapper.Internal;

namespace AppForeach.Framework.AutoMapper.Metadata;
internal class MappingMetadataProvider : IMappingMetadataProvider
{
    readonly IGlobalConfiguration globalConfiguration;
    public MappingMetadataProvider(IMapper mapper)
    {
        this.globalConfiguration = (IGlobalConfiguration)mapper.ConfigurationProvider;
    }
    public IEnumerable<IMappingMetadata> GetMappingMetadata(Type sourceType)
    {
        IEnumerable<TypeMap> typeMaps = globalConfiguration.GetAllTypeMaps().Where(x => x.SourceType == sourceType /*&& x.DestinationType == destinationType*/);

        return typeMaps.Select(t => new MappingMetadata
        {
            SourceType = t.SourceType,
            DestinationType = t.DestinationType,
            PropertyMaps = t.PropertyMaps.Where(x => x.SourceMember is not null).Select(x => new PropertyMap { SourceName = x.SourceMember.Name, DestinationName = x.DestinationName })
        }).ToList();
    }
}

