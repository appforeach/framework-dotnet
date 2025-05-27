using FluentValidation;
using FluentValidation.Validators;

namespace AppForeach.Framework.FluentValidation
{

    internal class NotEmptyButMayBeNullValidator<T, TProperty> : NotEmptyValidator<T, TProperty>
    {
        public override bool IsValid(ValidationContext<T> context, TProperty value)
        {
            if(value == null)
            {
                return true;
            }
            else
            {
                return base.IsValid(context, value);
            }
        }
    }
}
