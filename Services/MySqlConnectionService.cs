using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace ProjectPitch4.Services
{
    public class MySqlConnectionService
    {
        private readonly string _connectionString;
        private readonly string _salesStoreInfo;

        public MySqlConnectionService(IConfiguration configuration)
        {
            _connectionString = configuration["AppConfig:MySqlConnection"];
            _salesStoreInfo = configuration["AppConfig:MySqlTableName"];
        }

        public MySqlConnection GetConnection()
        {
            var connection = new MySqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        public string GetTableName()
        {
            return _salesStoreInfo;
        }
    }
}
