using AppForeach.Framework.Mapping;
using AutoMapper;

namespace AppForeach.Framework.AutoMapper
{
    internal class AutoMapperFrameworkMapper : IFrameworkMapper
    {
        private readonly IMapper mapper;

        public AutoMapperFrameworkMapper(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public object Map(object source, Type destinationType)
        {
            ArgumentNullException.ThrowIfNull(source, nameof(source));
            ArgumentNullException.ThrowIfNull(destinationType, nameof(source));

            return mapper.Map(source, source.GetType(), destinationType);
        }
    }
}
