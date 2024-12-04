using System;

namespace AppForeach.Framework.Validation
{
    public interface IValidatorMap
    {
        Type GetValidatorType(Type inputType);
    }
}
