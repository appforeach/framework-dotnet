using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace AppForeach.Framework.EntityFrameworkCore.PostgreSql;

public class PostgreSqlDbOptions
{
    public Action<NpgsqlDbContextOptionsBuilder>? DbOptions { get; set; }
}
