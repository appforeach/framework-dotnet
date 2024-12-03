using AppForeach.Framework.DataType.Facets;
using System;

namespace AppForeach.Framework.DataType
{
    internal class PrimitiveFieldSpecification<TType> : IPrimitiveFieldSpecification<TType>
    {
        public FacetBag Configuration { get; } = new FacetBag();

        public IPrimitiveFieldSpecification<TType> Is<TDataType>() where TDataType : IDataType
        {
            throw new NotImplementedException();
        }

        public IPrimitiveFieldSpecification<TType> IsRequired(bool required = true)
        {
            Configuration.Set(new FieldRequiredFacet { Required = required });
            return this;
        }

        public IPrimitiveFieldSpecification<TType> IsOptional(bool optional = true)
        {
            Configuration.Set(new FieldRequiredFacet { Required = !optional });
            return this;
        }
    }
}
