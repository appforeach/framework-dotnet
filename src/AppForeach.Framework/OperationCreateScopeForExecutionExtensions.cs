
namespace AppForeach.Framework
{
    public static class OperationCreateScopeForExecutionExtensions
    {
        public static IOperationBuilder OperationCreateScopeForExecution(this IOperationBuilder builder, bool createScopeForExecution)
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
