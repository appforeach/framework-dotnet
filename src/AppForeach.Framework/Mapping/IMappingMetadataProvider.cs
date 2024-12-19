using System;
using System.Collections.Generic;
using System.Text;

namespace AppForeach.Framework.Mapping
{
    public interface IMappingMetadataProvider
    {
        IMappingMetadata GetMappingMetadata(Type sourceType);
    }
}
