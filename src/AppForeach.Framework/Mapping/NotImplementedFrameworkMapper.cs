using System;

namespace AppForeach.Framework.Mapping
{
    internal class NotImplementedFrameworkMapper : IFrameworkMapper
    {
        public object Map(object source, Type destinationType)
            => throw new NotImplementedException();
    }
}
