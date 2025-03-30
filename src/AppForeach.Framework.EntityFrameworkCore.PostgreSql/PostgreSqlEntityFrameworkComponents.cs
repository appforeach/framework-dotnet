using AppForeach.Framework.DependencyInjection;

namespace AppForeach.Framework.EntityFrameworkCore.PostgreSql
{
    public class PostgreSqlEntityFrameworkComponents : FrameworkModule
    {
        public PostgreSqlEntityFrameworkComponents()
        {
            AssemblyNoDefaultRegistration();

            Scoped<IDbOptionsConfigurator, PostgreSqlDbOptionsConfigurator>();
        }
    }
}
