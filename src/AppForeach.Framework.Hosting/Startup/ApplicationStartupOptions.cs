using System;

namespace AppForeach.Framework.Hosting.Startup
{
    public class ApplicationStartupOptions
    {
        public Func<bool>? RunCondition { get; set; }

        public Func<bool>? ApplicationTerminateCondition { get; set; }
    }
}
