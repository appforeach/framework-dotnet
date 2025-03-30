using AppForeach.Framework.Mapping;

namespace AppForeach.Framework.AutoMapper.Metadata;
internal class PropertyMap : IPropertyMap
{
    public required string SourceName { get; init; }
    public required string DestinationName { get; init; }
}
