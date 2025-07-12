
using DOTNETPROJECT.DTOs;
using DOTNETPROJECT.Entities;

namespace DOTNETPROJECT.Repositiries
{
    public interface IUserData
    {
        Task<int> Create(UserEntity userEntity);

        Task<List<UserEntity>> GetAll();

        Task<UserEntity?> GetById(int id);

        Task<bool> Exists(int id);

        Task Update(UserEntity userEntity);

        Task Delete(int id);
    }
}