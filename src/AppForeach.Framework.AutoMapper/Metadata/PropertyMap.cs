using AppForeach.Framework.Mapping;

namespace AppForeach.Framework.AutoMapper.Metadata;
internal class PropertyMap : IPropertyMap
{
    public string SourceName { get; init; }
    public string DestinationName { get; init; }
}
