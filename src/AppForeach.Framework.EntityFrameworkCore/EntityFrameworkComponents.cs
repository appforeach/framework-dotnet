using AppForeach.Framework.DependencyInjection;
using AppForeach.Framework.EntityFrameworkCore.Audit;
using Microsoft.EntityFrameworkCore;

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
            Component(typeof(IDbContextFactory<>), typeof(FrameworkDbContextFactory<>), ComponentLifetime.Scoped);
        }
    }
}
