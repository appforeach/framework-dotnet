using System;

namespace AppForeach.Framework.Hosting.Startup
{
    public interface IApplicationStartupOptionsConfigurator
    {
        void RunIf(Func<bool> condition);

        void TerminateApplicationIf(Func<bool> condition);
    }
}
