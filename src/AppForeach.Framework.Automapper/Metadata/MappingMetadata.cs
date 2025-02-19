using AppForeach.Framework.Mapping;

namespace AppForeach.Framework.AutoMapper.Metadata;
internal class MappingMetadata : IMappingMetadata
{
    public Type DestinationType { get; init; }
    public Type SourceType { get; init; }
    public IEnumerable<IPropertyMap> PropertyMaps { get; init; }
}
