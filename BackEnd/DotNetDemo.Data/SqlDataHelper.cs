using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace DotNetDemo.Data
{
    public class SqlDataHelper
    {
        private string _connectionString;
        private readonly ILogger _logger;

        public SqlDataHelper(string connectionString, ILogger logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public async Task<DataTable> ExcuteQueryAsync(string query, CommandType commandType, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = commandType;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    await connection.OpenAsync();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        return dataTable;
                    }
                }
            }
        }

        public async Task<int> ExecuteNonQueryAsync(string query, CommandType commandType, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = commandType;
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        await connection.OpenAsync();
                        return await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Error {message}", e.Message);
                return -1;
            }
        }
    }
}