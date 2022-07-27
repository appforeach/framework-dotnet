
namespace AppForeach.Framework
{
    internal class OperationBuilder : IOperationBuilder
    {
        public OperationBuilder(FacetBag facetBag)
        {
            Configuration = facetBag;
        }

        public FacetBag Configuration { get; }
    }
}
