using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AppForeach.Framework.Mapping
{
    public interface IMappingMetadata
    {
        Type DestinationType { get; }
        Type SourceType { get; }
        IEnumerable<IPropertyMap> PropertyMaps { get; }
    }
}
