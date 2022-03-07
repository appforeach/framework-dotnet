using System;

namespace AppForeach.Framework
{
    public interface IUnhandledExceptionEventHandler
    {
        ExceptionHandlerResult OnUnhandledException(Exception ex);
    }
}
