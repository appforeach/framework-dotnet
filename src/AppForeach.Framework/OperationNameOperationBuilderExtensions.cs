
namespace AppForeach.Framework
{
    public static class OperationNameOperationBuilderExtensions
    {
        public static IOperationBuilder OperationName(this IOperationBuilder builder, string operationName)
        {
            var facet = new OperationNameFacet
            {
                OperationName = operationName
            };
            builder.Configuration.Set(facet);

            return builder;
        }

        public static IOperationBuilder IsCommand(this IOperationBuilder builder, bool isCommand)
        {
            var facet = new OperationIsCommandFacet
            {
                IsCommand = isCommand
            };
            builder.Configuration.Set(facet);

            return builder;
        }
    }
}
