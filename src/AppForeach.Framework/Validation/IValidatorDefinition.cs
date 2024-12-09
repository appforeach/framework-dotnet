using System;

namespace AppForeach.Framework.Validation
{
    public interface IValidatorDefinition
    {
        Type InputType  { get; set; }

        Type ValidatorType { get; set; }
    }
}
