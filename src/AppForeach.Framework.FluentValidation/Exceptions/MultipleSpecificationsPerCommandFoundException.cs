namespace AppForeach.Framework.FluentValidation.Exceptions;
public class MultipleSpecificationsPerCommandFoundException : Exception
{
    public MultipleSpecificationsPerCommandFoundException(string message) : base(message)
    {
    }
}
