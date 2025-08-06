using System;

namespace AppForeach.Framework.Hosting.Startup
{
    public class ApplicationStartupOptionsConfigurator 
        (
            ApplicationStartupOptions options
        ) : IApplicationStartupOptionsConfigurator
    {
        public void RunIf(Func<bool> condition)
        {
            options.RunCondition = condition;
        }

        public void TerminateApplicationIf(Func<bool> condition)
        {
            options.ApplicationTerminateCondition = condition;
        }
    }
}
