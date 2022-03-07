
namespace AppForeach.Framework
{
    public interface IValidationFailedEventHandler
    {
        void OnValidationFailed(OperationResult operationResult);
    }
}
