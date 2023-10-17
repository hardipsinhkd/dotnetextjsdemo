using DotNetDemo.Data;
using DotNetDemo.Service.Dtos;
using DotNetDemo.Service.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace DotNetDemo.Service
{
    public class UserService : IUserService
    {

        private readonly IConfiguration _configuration;
        private string _connectionString;
        private readonly ILogger<UserService> _logger;

        public UserService(IConfiguration configuration, ILogger<UserService> logger)
        {
            _configuration = configuration;
            _connectionString = _configuration["ConnectionStrings:Default"];
            _logger = logger;
        }

        public async Task<IEnumerable<GetUserDto>> GetAll()
        {
            SqlDataHelper sqlHelper = new SqlDataHelper(_connectionString, _logger);

            string allRecordsQ = "SELECT U.*, G.Name AS GroupName FROM Users U LEFT JOIN Groups G ON U.GroupId = G.Id";

            DataTable dt = await sqlHelper.ExcuteQueryAsync(allRecordsQ, CommandType.Text);

            List<GetUserDto> result = DataTableMapper.MapDataTableToList<GetUserDto>(dt);

            return result;
        }

        public async Task<GetUserDto> Get(int id)
        {
            SqlDataHelper sqlHelper = new SqlDataHelper(_connectionString, _logger);
            
            string singleRecordQ = "SELECT U.*, G.Name AS GroupName FROM Users U LEFT JOIN Groups G ON U.GroupId = G.Id WHERE U.Id = @Id";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Id", id),
            };

            DataTable dt = await sqlHelper.ExcuteQueryAsync(singleRecordQ, CommandType.Text, parameters);

            List<GetUserDto> result = DataTableMapper.MapDataTableToList<GetUserDto>(dt);

            return result.FirstOrDefault();
        }

        public async Task<int> Create(CreateUserDto input)
        {
            SqlDataHelper sqlHelper = new SqlDataHelper(_connectionString, _logger);

            string insertQ = @"INSERT INTO Users ([FirstName], [LastName], [EmailAddress], [Address])
                                VALUES (@FirstName, @LastName, @EmailAddress, @Address)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@FirstName", input.FirstName),
                new SqlParameter("@LastName", input.LastName),
                new SqlParameter("@EmailAddress", input.EmailAddress),
                new SqlParameter("@Address", input.Address),
            };

             return await sqlHelper.ExecuteNonQueryAsync(insertQ, CommandType.Text, parameters);
        }

        public async Task<int> Update(UpdateUserDto input)
        {
            SqlDataHelper sqlHelper = new SqlDataHelper(_connectionString, _logger);

            string updateQ = @"UPDATE Users 
                                SET FirstName = @FirstName, 
                                LastName = @LastName, 
                                EmailAddress = @EmailAddress,
                                Address = @Address
                                WHERE Id = @Id";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Id", input.Id),
                new SqlParameter("@FirstName", input.FirstName),
                new SqlParameter("@LastName", input.LastName),
                new SqlParameter("@EmailAddress", input.EmailAddress),
                new SqlParameter("@Address", input.Address),
            };

            return await sqlHelper.ExecuteNonQueryAsync(updateQ, CommandType.Text, parameters);
        }

        public async Task<int> Delete(int id)
        {
            SqlDataHelper sqlHelper = new SqlDataHelper(_connectionString, _logger);

            string deleteQ = "DELETE FROM Users WHERE Id = @Id";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Id", id),
            };

            return await sqlHelper.ExecuteNonQueryAsync(deleteQ, CommandType.Text, parameters);
        }

        public async Task<int> AssignGroup(AssignGroupToUserDto input)
        {
            SqlDataHelper sqlHelper = new SqlDataHelper(_connectionString, _logger);

            string updateQ = @"UPDATE Users 
                                SET GroupId = @GroupId
                                WHERE Id = @Id";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Id", input.Id),
                new SqlParameter("@GroupId", input.GroupId),
            };

            return await sqlHelper.ExecuteNonQueryAsync(updateQ, CommandType.Text, parameters);
        }
    }
}
