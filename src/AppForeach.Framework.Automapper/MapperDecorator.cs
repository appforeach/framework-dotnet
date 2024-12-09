namespace AppForeach.Framework.Automapper;
internal class MapperDecorator : IMapper
{
    private readonly AutoMapper.IMapper mapper;
    public MapperDecorator(AutoMapper.IMapper mapper)
    {
        this.mapper = mapper;
    }
    public TDestination Map<TSource, TDestination>(TSource source)
    {
        return mapper.Map<TSource, TDestination>(source);
    }

    public TDestination Map<TDestination>(object source)
    {
        return mapper.Map<TDestination>(source);
    }
}