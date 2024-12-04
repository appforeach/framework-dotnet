namespace AppForeach.Framework.Validation
{
    public interface IValidator
    {
        OperationResult Validate(object input);
    }
}
