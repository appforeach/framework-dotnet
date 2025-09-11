using System;

namespace AppForeach.Framework.Mapping
{
    public interface IFrameworkMapper
    {
        object Map(object source, Type destinationType);
    }
}
