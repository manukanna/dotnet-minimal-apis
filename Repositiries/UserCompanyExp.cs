using System.Data;
using Dapper;
using DOTNETPROJECT.Entities;
using Microsoft.Data.SqlClient;

namespace DOTNETPROJECT.Repositiries
{
    public class UserCompanyExp : IUserCompanyexp
    {
        private readonly string connectionString;

        public UserCompanyExp(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
        public async Task<int> CreateUserExp(UserExpEntity userExpEntity)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var id = await connection.QuerySingleAsync<int>("ExpCreateUser",
                new
                {
                    userExpEntity.CurrentCompanyName,
                    userExpEntity.CurrentCompanyExp,
                    userExpEntity.PrevCompanyName,
                    userExpEntity.TotalYearsExp,
                    userExpEntity.AllowRelocate,
                    userExpEntity.NotciePeriod,
                    userExpEntity.CurrentLocation
                }, commandType: CommandType.StoredProcedure);
                userExpEntity.Id = id;
                return id;
            }
        }
        public async Task<List<UserExpEntity>> GetAllUsersExp()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var userExpEntity = await connection.QueryAsync<UserExpEntity>("ExpGetAllUserData", commandType: CommandType.StoredProcedure);
                return userExpEntity.ToList();
            }
        }

        public async Task<UserExpEntity?> GetById(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var userData = await connection.QueryFirstOrDefaultAsync<UserExpEntity>("ExpGetUserById", new { id }, commandType: CommandType.StoredProcedure);
                return userData;
            }
        }

        public async Task UpdateUserExp(UserExpEntity userExpEntity)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync("ExpUpdateUser", userExpEntity, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<bool> UserExpExists(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var userExpExists = await connection.QuerySingleAsync<bool>("ExpUserGetExists", new { id }, commandType: CommandType.StoredProcedure);
                return userExpExists;
            }
        }

        public async Task DeleteUserExp(int id)
        {
            using (var connection = new SqlConnection(connectionString)){
                await connection.ExecuteAsync("ExpDeleteUser", new { id }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}