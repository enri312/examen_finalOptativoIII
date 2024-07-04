using System.Data;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace SuperProjectDapper.Repositories.Data
{
    public class DBConnections
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DBConnections(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
