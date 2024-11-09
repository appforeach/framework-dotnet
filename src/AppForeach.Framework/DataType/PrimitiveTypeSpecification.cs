using System;

namespace AppForeach.Framework.DataType
{
    internal class PrimitiveTypeSpecification<TType> : IPrimitiveTypeSpecification<TType>
    {
        private bool _optional = false;
        private bool _required = false;

        public IPrimitiveTypeSpecification<TType> Is<TDataType>() where TDataType : IDataType
        {
            throw new NotImplementedException();
        }

        public IPrimitiveTypeSpecification<TType> IsOptional()
        {
            _optional = true;
            return this;
        }

        public IPrimitiveTypeSpecification<TType> IsRequired()
        {
            _required = true;
            return this;
        }
    }
}
