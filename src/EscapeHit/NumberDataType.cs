using AppForeach.Framework.DataType;

namespace EscapeHit
{
    public class NumberDataType : BaseDataType<string>
    {
        public NumberDataType()
        {
            Is().MaxLength(6).Pattern(@"^\d+$");
        }
    }
}
