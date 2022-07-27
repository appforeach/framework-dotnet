
namespace AppForeach.Framework
{
    public class OperationContextState
    {
        public string OperationName { get; set; }

        public bool IsCommand { get; set; }

        public bool IsOperationNameResolved { get; set; }

        public object Input { get; set; }

        public FacetBag Configuration { get; set; }

        public bool IsOperationInputSet { get; set;  }
    }
}
