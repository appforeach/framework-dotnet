using AppForeach.Framework.Mapping;

namespace AppForeach.Framework.Automapper.Metadata;
internal class PropertyMap : IPropertyMap
{
    public string SourceName { get; init; }
    public string DestinationName { get; init; }
}
