
namespace AppForeach.Framework.EntityFrameworkCore
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        public ConnectionStringProvider(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; }
    }
}
