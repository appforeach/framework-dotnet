using System;

namespace AppForeach.Framework
{
    public class OperationNameResolver : IOperationNameResolver
    {
        public OperationName ResolveName(Type inputType, Type handlerType)
        {
            var operationName = new OperationName();

            if (inputType.Name.EndsWith("Command"))
            {
                operationName.Name = inputType.Name.Substring(0, inputType.Name.Length - "Command".Length);
                operationName.IsCommand = true;
            }
            else if (inputType.Name.EndsWith("Query"))
            {
                operationName.Name = inputType.Name.Substring(0, inputType.Name.Length - "Query".Length);
                operationName.IsCommand = false;
            }
            else
            {
                throw new FrameworkException("Unable to determine operation name and command/query specification.");
            }

            return operationName;
        }
    }
}
