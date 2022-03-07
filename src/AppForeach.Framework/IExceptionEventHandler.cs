using System;

namespace AppForeach.Framework
{
    public interface IExceptionEventHandler
    {
        ExceptionHandlerResult OnException(Exception ex);
    }
}
