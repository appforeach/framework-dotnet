using AppForeach.Framework.Mapping;
using AutoMapper;

namespace AppForeach.Framework.Automapper.Metadata;
internal class MappingMetadataProvider : IMappingMetadataProvider
{
    readonly IConfigurationProvider configurationProvider;
    public MappingMetadataProvider(IConfigurationProvider configurationProvider)
    {
        this.configurationProvider = configurationProvider;
    }
    public IMappingMetadata GetMappingMetadata(Type sourceType)
    {
        TypeMap? typeMap = configurationProvider.GetAllTypeMaps()?.FirstOrDefault(x => x.SourceType == sourceType /*&& x.DestinationType == destinationType*/);

        var propertyMaps = typeMap.PropertyMaps.Where(x => x.HasSource).Select(x => new PropertyMap { SourceName = x.SourceMember.Name, DestinationName = x.DestinationName });

        var metadata = new MappingMetadata
        {
            SourceType = typeMap.SourceType,
            DestinationType = typeMap.DestinationType,
            PropertyMaps = propertyMaps
        };

        return metadata;
    } 
}

