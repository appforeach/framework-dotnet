using System;

namespace AppForeach.Framework.DataType
{
    internal class PrimitiveTypeSpecification<TType> : IPrimitiveTypeSpecification<TType>
    {
        private bool _required = false;
        private bool _optional = false;

        public IPrimitiveTypeSpecification<TType> Is<TDataType>() where TDataType : IDataType
        {
            throw new NotImplementedException();
        }

        public IPrimitiveTypeSpecification<TType> IsRequired(bool required = true)
        {
            _required = required;
            return this;
        }

        //TODO: no we need to remove this method? Seems like negation of IsRequired
        public IPrimitiveTypeSpecification<TType> IsOptional(bool optional = true)
        {
            _optional = optional;
            return this;
        }

        public IPrimitiveTypeSpecificationConfig Config =>
            new PrimitiveTypeSpecificationConfig(required: _required, optional: _optional);

    }
}
