namespace AppForeach.Framework.DataType
{
    public interface IPrimitiveTypeSpecification<TType>
    {
        IPrimitiveTypeSpecification<TType> IsRequired(bool required = true);

        IPrimitiveTypeSpecification<TType> IsOptional(bool optional = true);

        IPrimitiveTypeSpecification<TType> Is<TDataType>() where TDataType : IDataType;

        IPrimitiveTypeSpecificationConfig Config { get; }
    }
}
