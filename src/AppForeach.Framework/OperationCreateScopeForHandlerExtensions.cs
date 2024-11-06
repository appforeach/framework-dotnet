
namespace AppForeach.Framework
{
    public static class OperationCreateScopeForHandlerExtensions
    {
        public static IOperationBuilder OperationCreateScopeForHandler(this IOperationBuilder builder, bool createScopeForExecution)
        {
            var facet = new OperationCreateScopeForExecutionFacet
            {
                CreateScopeForExecution = createScopeForExecution
            };
            builder.Configuration.Set(facet);

            return builder;
        }
    }
}
