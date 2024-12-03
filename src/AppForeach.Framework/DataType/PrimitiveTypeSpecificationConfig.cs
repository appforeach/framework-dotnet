namespace AppForeach.Framework.DataType
{
    public interface IPrimitiveTypeSpecificationConfig
    {
        bool IsOptional { get; }
        bool IsRequired { get; }
    }

    //TODO: discuss naming
    public class PrimitiveTypeSpecificationConfig : IPrimitiveTypeSpecificationConfig
    {
        public PrimitiveTypeSpecificationConfig(bool required, bool optional)
        {
            IsRequired = required;
            IsOptional = optional;
        }
        public bool IsRequired { get; }
        public bool IsOptional { get; }

        // Can't user this
        // public bool IsRequired { get; init; }
    }
}
