
namespace AppForeach.Framework
{
    public class OperationContext : IOperationContext
    {
        private readonly IOperationState operationState;

        public OperationContext(IOperationState operationState)
        {
            this.operationState = operationState;
        }

        public string OperationName
        {
            get
            {
                EnsureOperationNameResolved();
                return ContextState.OperationName;
            }
        }

        public bool IsCommand
        {
            get
            {
                EnsureOperationNameResolved();
                return ContextState.IsCommand;
            }
        }

        public object Input
        {
            get
            {
                EnsureOperationInputSet();
                return ContextState.Input;
            }
        }

        public IBag Configuration
        {
            get
            {
                EnsureOperationInputSet();
                return ContextState.Configuration;
            }
        }

        public IBag State => operationState.State;

        private OperationContextState ContextState => State.Get<OperationContextState>();

        private void EnsureOperationNameResolved()
        {
            if (!ContextState.IsOperationNameResolved)
            {
                throw new FrameworkException("Operation name is not resolved.");
            }
        }

        private void EnsureOperationInputSet()
        {
            if (!ContextState.IsOperationInputSet)
            {
                throw new FrameworkException("Operation input is not set.");
            }
        }
    }
}
