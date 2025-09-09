using AppForeach.Framework.Hosting.Features.Sql;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AppForeach.Framework.Hosting.Features.SqlServer;

public interface ISqlServerOptionsConfigurator : ISqlOptionsConfigurator
{
    void ExecutionDbContextOptions(Action<SqlServerDbContextOptionsBuilder> options);

    void MigrationDbContextOptions(Action<SqlServerDbContextOptionsBuilder> options);

    void FrameworkMigrationDbContextOptions(Action<SqlServerDbContextOptionsBuilder> options);
}
