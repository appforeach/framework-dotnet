using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AppForeach.Framework.Microsoft.Extensions.DependencyInjection;
using AppForeach.Framework.EntityFrameworkCore;
using AppForeach.Framework.Hosting.Features.Sql;
using AppForeach.Framework.EntityFrameworkCore.PostgreSql;

namespace AppForeach.Framework.Hosting.Features.PostgreSql;

public class PostgreSqlFeatureInstaller<TDbContext> : SqlFeatureInstaller<TDbContext>
    where TDbContext : DbContext
{
    private readonly PostgreSqlFeatureOption<TDbContext> option;

    public PostgreSqlFeatureInstaller(PostgreSqlFeatureOption<TDbContext> option) : base(option)
    {
        this.option = option;
    }

    public override void SetUpServices(IApplicationFeatureInstallContext installContext, IServiceCollection services)
    {
        base.SetUpServices(installContext, services);


        services.AddFrameworkModule<PostgreSqlEntityFrameworkComponents>();

        services.AddApplicationStartup<PostgreSqlMigrationStartup<TDbContext>>();
        services.Configure<PostgreSqlMigrationOptions<TDbContext>>(opt => opt.ConnectionString = connectionString);

        services.AddApplicationStartup<PostgreSqlMigrationStartup<FrameworkDbContext>>();
        services.Configure<PostgreSqlMigrationOptions<FrameworkDbContext>>(opt =>
        {
            opt.ConnectionString = connectionString;
            opt.DbContextOptions = opt => opt
                .MigrationsHistoryTable("__FrameworkEFMigrationsHistory", "framework")
                .MigrationsAssembly(typeof(PostgreSqlEntityFrameworkComponents).Assembly.FullName);
        });

    }
}
