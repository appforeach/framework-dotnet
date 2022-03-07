using System;

namespace AppForeach.Framework
{
    public interface IValidatorMap
    {
        Type GetValidatorType(Type inputType);
    }
}
