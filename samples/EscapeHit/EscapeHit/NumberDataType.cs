using AppForeach.Framework.DataType;

namespace EscapeHit
{
    public class NumberDataType : BaseDataType<string>
    {
        public NumberDataType()
        {
            Is().HasMaxLength(6);
        }
    }
}
