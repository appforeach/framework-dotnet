using AppForeach.Framework.DependencyInjection;
using AppForeach.Framework.Logging;
using Serilog.Core;

namespace AppForeach.Framework.Serilog
{
    public class SerilogFrameworkComponents : FrameworkModule
    {
        public SerilogFrameworkComponents()
        {
            Transient<IFrameworkLogger, SerilogFrameworkLogger>();
            Singleton<ILogEventEnricher, SerilogFrameworkAggregatedPropertyProvider>();
        }
    }
}
