namespace AppForeach.Framework.DataType
{
    public interface IPrimitiveTypeSpecification<TType>
    {
        IPrimitiveTypeSpecification<TType> IsRequired();

        IPrimitiveTypeSpecification<TType> IsOptional();

        IPrimitiveTypeSpecification<TType> Is<TDataType>() where TDataType : IDataType;
    }
}
