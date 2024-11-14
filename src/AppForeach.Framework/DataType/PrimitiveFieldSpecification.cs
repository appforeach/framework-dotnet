using AppForeach.Framework.DataType.Facets;
using System;

namespace AppForeach.Framework.DataType
{
    internal class PrimitiveFieldSpecification<TType> : IPrimitiveTypeSpecification<TType>
    {
        public FacetBag Configuration { get; } = new FacetBag();

        public IPrimitiveTypeSpecification<TType> Is<TDataType>() where TDataType : IDataType
        {
            throw new NotImplementedException();
        }

        public IPrimitiveTypeSpecification<TType> IsRequired(bool required = true)
        {
            Configuration.Set(new FieldRequiredFacet { Required = required });
            return this;
        }

        public IPrimitiveTypeSpecification<TType> IsOptional(bool optional = true)
        {
            Configuration.Set(new FieldRequiredFacet { Required = !optional });
            return this;
        }
    }
}
