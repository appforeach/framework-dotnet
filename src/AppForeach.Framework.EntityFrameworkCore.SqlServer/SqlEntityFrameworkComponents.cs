using AppForeach.Framework.DependencyInjection;

namespace AppForeach.Framework.EntityFrameworkCore.SqlServer
{
    public class SqlEntityFrameworkComponents : FrameworkModule
    {
        public SqlEntityFrameworkComponents()
        {
            AssemblyNoDefaultRegistration();

            Scoped<IDbOptionsConfigurator, SqlDbOptionsConfigurator>();
        }
    }
}
