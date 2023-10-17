using DotNetDemo.Data;
using DotNetDemo.Service.Dtos;
using DotNetDemo.Service.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace DotNetDemo.Service
{
    public class GroupService : IGroupService
    {
        private readonly IConfiguration _configuration;
        private string _connectionString;
        private readonly ILogger<GroupService> _logger;

        public GroupService(IConfiguration configuration, ILogger<GroupService> logger)
        {
            _configuration = configuration;
            _connectionString = _configuration["ConnectionStrings:Default"];
            _logger = logger;
        }

        public async Task<IEnumerable<GetGroupDto>> GetAll()
        {
            SqlDataHelper sqlHelper = new SqlDataHelper(_connectionString, _logger);

            string allRecordsQ = "SELECT * FROM Groups";

            DataTable dt = await sqlHelper.ExcuteQueryAsync(allRecordsQ, CommandType.Text);

            List<GetGroupDto> result = DataTableMapper.MapDataTableToList<GetGroupDto>(dt);

            return result;
        }

        public async Task<GetGroupDto> Get(int id)
        {
            SqlDataHelper sqlHelper = new SqlDataHelper(_connectionString, _logger);

            string singleRecordQ = "SELECT * FROM Groups WHERE Id = @Id";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Id", id),
            };

            DataTable dt = await sqlHelper.ExcuteQueryAsync(singleRecordQ, CommandType.Text, parameters);

            List<GetGroupDto> result = DataTableMapper.MapDataTableToList<GetGroupDto>(dt);

            return result.FirstOrDefault();
        }

        public async Task<int> Create(CreateGroupDto input)
        {
            SqlDataHelper sqlHelper = new SqlDataHelper(_connectionString, _logger);

            string insertQ = "INSERT INTO Groups VALUES (@Name)";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Name", input.Name)
            };

            return await sqlHelper.ExecuteNonQueryAsync(insertQ, CommandType.Text, parameters);
        }

        public async Task<int> Update(UpdateGroupDto input)
        {
            SqlDataHelper sqlHelper = new SqlDataHelper(_connectionString, _logger);

            string updateQ = "UPDATE Groups SET Name = @Name WHERE Id = @Id";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Id", input.Id),
                new SqlParameter("@Name", input.Name),
            };

            return await sqlHelper.ExecuteNonQueryAsync(updateQ, CommandType.Text, parameters);
        }

        public async Task<int> Delete(int id)
        {
            SqlDataHelper sqlHelper = new SqlDataHelper(_connectionString, _logger);

            string deleteQ = "DELETE FROM Groups WHERE Id = @Id";

            SqlParameter[] parameters =
            {
                new SqlParameter("@Id", id),
            };

            return await sqlHelper.ExecuteNonQueryAsync(deleteQ, CommandType.Text, parameters);
        }

        public async Task<IEnumerable<GetGroupDto>> GetAllForAssigning()
        {
            SqlDataHelper sqlHelper = new SqlDataHelper(_connectionString, _logger);

            string allRecordsQ = "SELECT * FROM Groups";

            DataTable dt = await sqlHelper.ExcuteQueryAsync(allRecordsQ, CommandType.Text);

            List<GetGroupDto> result = DataTableMapper.MapDataTableToList<GetGroupDto>(dt);

            return result;
        }
    }
}