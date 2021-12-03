
namespace AppForeach.Framework.DataType
{
    public class BasePrimitiveTypeSpecification
    {
        protected IPrimitiveTypeSpecification<TType> Type<TType>()
        {
            return null;
        }

        protected void ByDefaultRequired()
        {

        }

        protected void ByDefaultNotRequired()
        {
        }
    }
}
