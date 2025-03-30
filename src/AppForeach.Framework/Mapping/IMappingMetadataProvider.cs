using System;
using System.Collections.Generic;

namespace AppForeach.Framework.Mapping
{
    public interface IMappingMetadataProvider
    {
        IEnumerable<IMappingMetadata> GetMappingMetadata(Type sourceType);
    }
}
