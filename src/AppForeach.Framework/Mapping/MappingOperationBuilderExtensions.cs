namespace AppForeach.Framework.Mapping
{
    public static class MappingOperationBuilderExtensions
    {
        public static IOperationBuilder MapTo<TDestination>(this IOperationBuilder builder)
        {
            var facet = new MappingDestinationTypeFacet
            {
                DestinationType = typeof(TDestination)
            };

            builder.Configuration.Set(facet);

            return builder;
        }
    }
}
