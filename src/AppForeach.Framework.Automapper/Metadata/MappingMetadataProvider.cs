using AppForeach.Framework.Mapping;
using AutoMapper;

namespace AppForeach.Framework.AutoMapper.Metadata;
internal class MappingMetadataProvider : IMappingMetadataProvider
{
    readonly IConfigurationProvider configurationProvider;
    public MappingMetadataProvider(IConfigurationProvider configurationProvider)
    {
        this.configurationProvider = configurationProvider;
    }
    public IEnumerable<IMappingMetadata> GetMappingMetadata(Type sourceType)
    {
        IEnumerable<TypeMap> typeMaps = configurationProvider.GetAllTypeMaps().Where(x => x.SourceType == sourceType /*&& x.DestinationType == destinationType*/);

        return typeMaps.Select(t => new MappingMetadata
        {
            SourceType = t.SourceType,
            DestinationType = t.DestinationType,
            PropertyMaps = t.PropertyMaps.Where(x => x.HasSource).Select(x => new PropertyMap { SourceName = x.SourceMember.Name, DestinationName = x.DestinationName })
        }).ToList();
    }
}

