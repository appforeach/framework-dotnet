
namespace AppForeach.Framework.DataType
{
    public static class StringTypeSpecificationExtensions
    {
        public static IPrimitiveTypeSpecification<string> MaxLength(this IPrimitiveTypeSpecification<string> spec, int maxLength) => null;

        public static IPrimitiveTypeSpecification<string> Pattern(this IPrimitiveTypeSpecification<string> spec, string pattern) => null;
    }
}
