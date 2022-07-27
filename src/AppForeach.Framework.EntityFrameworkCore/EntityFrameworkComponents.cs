using AppForeach.Framework.DependencyInjection;

namespace AppForeach.Framework.EntityFrameworkCore
{
    public class EntityFrameworkComponents : FrameworkModule
    {
        public EntityFrameworkComponents()
        {
            AssemblyNoDefaultRegistration();

            Scoped<TransactionScopeMiddleware, TransactionScopeMiddleware>();
            Scoped<IDbContextActivator, DbContextActivator>();
        }
    }
}
