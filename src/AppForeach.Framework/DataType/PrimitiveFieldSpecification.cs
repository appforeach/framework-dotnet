using AppForeach.Framework.DataType.Facets;
using System;

namespace AppForeach.Framework.DataType
{
    internal class PrimitiveFieldSpecification<TType> : PrimitiveFieldSpecification, IPrimitiveFieldSpecification<TType>
    {
        public PrimitiveFieldSpecification(FacetBag backedConfiguration)
            : base(backedConfiguration)
        {
        }

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

    internal class PrimitiveFieldSpecification : IPrimitiveFieldSpecification
    {
        public FacetBag Configuration { get; }

        public PrimitiveFieldSpecification(FacetBag backedConfiguration)
        {
            Configuration = backedConfiguration;
        }
    }
}
