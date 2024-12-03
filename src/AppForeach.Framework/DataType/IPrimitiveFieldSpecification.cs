namespace AppForeach.Framework.DataType
{
    public interface IPrimitiveFieldSpecification<TType> : IPrimitiveFieldSpecification
    {
        IPrimitiveFieldSpecification<TType> IsRequired(bool required = true);

        IPrimitiveFieldSpecification<TType> IsOptional(bool optional = true);

        IPrimitiveFieldSpecification<TType> Is<TDataType>() where TDataType : IDataType;
    }

    public interface IPrimitiveFieldSpecification
    {
        FacetBag Configuration { get; }
    }
}
