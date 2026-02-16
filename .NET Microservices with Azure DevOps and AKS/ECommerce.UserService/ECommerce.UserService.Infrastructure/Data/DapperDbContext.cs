
using Microsoft.Extensions.Configuration;

namespace ECommerce.UserService.Infrastructure.Data
{
    public class DapperDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly Npgsql.NpgsqlConnection _dbConnection;
        public Npgsql.NpgsqlConnection DbConnection => _dbConnection;

        public DapperDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = _configuration.GetConnectionString("PostgresConnection");
            _dbConnection = new Npgsql.NpgsqlConnection(connectionString);
        }
    }
}
