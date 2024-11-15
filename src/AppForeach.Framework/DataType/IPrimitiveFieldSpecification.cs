namespace AppForeach.Framework.DataType
{
    public interface IPrimitiveFieldSpecification<TType>
    {
        IPrimitiveFieldSpecification<TType> IsRequired(bool required = true);

        IPrimitiveFieldSpecification<TType> IsOptional(bool optional = true);

        IPrimitiveFieldSpecification<TType> Is<TDataType>() where TDataType : IDataType;

        FacetBag Configuration{ get; }
    }
}
