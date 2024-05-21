using AppForeach.Framework.DependencyInjection;
using AppForeach.Framework.EntityFrameworkCore.Audit;

namespace AppForeach.Framework.EntityFrameworkCore
{
    public class EntityFrameworkComponents : FrameworkModule
    {
        public EntityFrameworkComponents()
        {
            AssemblyNoDefaultRegistration();

            Scoped<TransactionScopeMiddleware, TransactionScopeMiddleware>();
            Scoped<AuditMiddleware, AuditMiddleware>();
            Scoped<IDbContextActivator, DbContextActivator>();
            Scoped<IDbContextInternalActivator, DbContextInternalActivator>();
        }
    }
}
