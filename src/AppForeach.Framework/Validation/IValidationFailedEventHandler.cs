namespace AppForeach.Framework.Validation
{
    public interface IValidationFailedEventHandler
    {
        void OnValidationFailed(OperationResult operationResult);
    }
}
