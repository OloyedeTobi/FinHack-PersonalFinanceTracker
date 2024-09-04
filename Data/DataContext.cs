using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace FinanceTracker.Data
{
    public class DataContext(IConfiguration config)
    {
        private readonly IConfiguration _config = config;

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        }

        public IEnumerable<T> QueryData<T>(string sql, object? parameters = null)
        {
            using var connection = CreateConnection();
            return connection.Query<T>(sql, parameters);
        }

        public T QuerySingle<T>(string sql, object? parameters = null)
        {
            using var connection = CreateConnection();
            return connection.QuerySingle<T>(sql, parameters);
        }

         public T QuerySingleOrDefault<T>(string sql, object? parameters = null)
        {
            using var connection = CreateConnection();
            return connection.QuerySingleOrDefault<T>(sql, parameters);
        }

        public async Task<T> QuerySingleOrDefaultAsync<T>(string sql, object? parameters = null)
        {
            using var connection = CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<T>(sql, parameters);
        }


        public bool ExecuteCommand(string sql, object? parameters = null)
        {
            using var connection = CreateConnection();
            
            return connection.Execute(sql, parameters) > 0;
        }

        public async Task<bool> ExecuteAsync(string sql, object? parameters = null)
        {
            using var connection = CreateConnection();
            return await connection.ExecuteAsync(sql, parameters) > 0;
        }
    }
}
