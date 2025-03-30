using System.Linq.Expressions;

namespace AppForeach.Framework.FluentValidation.Extensions;
public class ValidationOptions<TType>
{
    private readonly List<string> _propertiesToSkip = new List<string>();

    public static ValidationOptions<TType> Default() =>
        new ValidationOptions<TType>();

    public bool ShouldSkip(string fieldKey) =>
        _propertiesToSkip.Contains(fieldKey);

    public ValidationOptions<TType> Skip<TFieldType>(Expression<Func<TType, TFieldType>> selector)
    {
        var fieldKey = ((MemberExpression)selector.Body).Member.Name;

        _propertiesToSkip.Add(fieldKey);

        return this;
    }
}