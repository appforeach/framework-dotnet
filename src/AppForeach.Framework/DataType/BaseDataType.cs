
namespace AppForeach.Framework.DataType
{
    public class BaseDataType<TPrimitive> : IDataType
    {
        protected IPrimitiveTypeSpecification<TPrimitive> Is() => null;
    }
}
