using Autofac;
using System;

namespace AppForeach.Framework.EntityFrameworkCore.Autofac
{
    public static class ContainerBuilderRegistrationExtensions
    {
        public static void RegisterEntityFrameworkFeatures(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<TransactionScopeMiddleware>().InstancePerLifetimeScope();

            containerBuilder.RegisterType<DbContextActivator>().As<IDbContextActivator>().InstancePerLifetimeScope();
        }
    }
}
