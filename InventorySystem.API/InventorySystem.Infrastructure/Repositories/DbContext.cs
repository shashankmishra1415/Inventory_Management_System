using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace InventorySystem.Infrastructure.Repositories
{
    public class DbContext
    {
        private readonly MySqlConnection connection;
        public DbContext(IConfiguration configuration)
        {
            this.connection = new MySqlConnection(configuration.GetConnectionString("DBConnectionString"));
        }
        public MySqlConnection GetConnection()
        {
            return connection;
        }
    }
}
