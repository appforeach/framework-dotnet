using System;

namespace AppForeach.Framework
{
    public class DefaultExceptionEventHandler : IExceptionEventHandler
    {
        public ExceptionHandlerResult OnException(Exception ex)
        {
            return new ExceptionHandlerResult { IsHandled = false };
        }
    }
}
