
namespace AppForeach.Framework.DataType
{
    public class BaseDataType<TPrimitive> : IDataType
    {
        protected IPrimitiveFieldSpecification<TPrimitive> Is() => null;
    }
}
