namespace FluentValidation;

public static class FluentValidatorExtensions
{
    public static IRuleBuilderInitial<T, TProperty> RuleFor<T, TProperty>(this AbstractValidator<T> validator, string propertyName)
    {
        return validator.RuleFor<TProperty>(x => GetPropertyValue<TProperty>(x!, propertyName));
    }

    private static TProperty GetPropertyValue<TProperty>(object obj, string propertyName)
    {
        var propertyInfo = obj.GetType().GetProperty(propertyName);
        return (TProperty)propertyInfo?.GetValue(obj, null)!;
    }

    public static IRuleBuilderInitial<T, object> RuleFor<T>(this AbstractValidator<T> validator, string propertyName)
    {
        return validator.RuleFor<object>(x => GetPropertyValue(x!, propertyName));
    }

    private static object GetPropertyValue(object obj, string propertyName)
    {
        var propertyInfo = obj.GetType().GetProperty(propertyName);
        return propertyInfo?.GetValue(obj, null)!;
    }
}