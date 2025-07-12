

using DOTNETPROJECT.Entities;

namespace DOTNETPROJECT.Repositiries {
    public interface IUserCompanyexp
    {
        Task<int> CreateUserExp(UserExpEntity userExpEntity);
        Task<List<UserExpEntity>> GetAllUsersExp();
        Task UpdateUserExp(UserExpEntity userExpEntity);
        Task<bool> UserExpExists(int id);
        Task<UserExpEntity?> GetById(int id);
        Task DeleteUserExp(int id);
    }
}