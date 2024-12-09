using System;

namespace AppForeach.Framework.Validation
{
    public class ValidatorDefinition : IValidatorDefinition
    {
        public Type InputType { get; set; }

        public Type ValidatorType { get; set; }
    }
}
