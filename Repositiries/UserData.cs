using System.Data;
using Dapper;
using DOTNETPROJECT.Entities;

using Microsoft.Data.SqlClient;

namespace DOTNETPROJECT.Repositiries
{
    public class UserData : IUserData
    {
        private readonly string connectionString;

        public UserData(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
        public async Task<int> Create(UserEntity userEntity)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var id = await connection.QuerySingleAsync<int>("CreateNewUser", new { userEntity.UserName, userEntity.LastName, userEntity.Phone}, commandType: CommandType.StoredProcedure);
                userEntity.Id = id;
                return id;
            }
        }

        public async Task Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync("DeleteUser", new {id}, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<bool> Exists(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var userExists = await connection.QuerySingleAsync<bool>("GetExists", new {id}, commandType: CommandType.StoredProcedure);
                return userExists;
            }
        }

        public async Task<List<UserEntity>> GetAll()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var userEntity = await connection.QueryAsync<UserEntity>("GetAllUserData", commandType: CommandType.StoredProcedure);
                return userEntity.ToList();
            }
        }

        public async Task<UserEntity?> GetById(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var userEntity = await connection.QueryFirstOrDefaultAsync<UserEntity>("GetUserById", new { id }, commandType: CommandType.StoredProcedure);

                return userEntity;

            }
        }

        public async Task Update(UserEntity userEntity)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var updateUser = await connection.ExecuteAsync("UpdateUser", userEntity, commandType:CommandType.StoredProcedure);
            }
        }
    }
}