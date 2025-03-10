using AppForeach.Framework.Mapping;

namespace AppForeach.Framework.AutoMapper.Metadata;
internal class MappingMetadata : IMappingMetadata
{
    public required Type DestinationType { get; init; }
    public required Type SourceType { get; init; }
    public required IEnumerable<IPropertyMap> PropertyMaps { get; init; }
}
