using BW2_Team6.Models;

namespace BW2_Team6.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User> Create(UserViewModel entity, IEnumerable<int> roleSelected);
        Task<User> Delete(int id);

        Task<IEnumerable<User>> GetAll();

        Task<User> GetById(int id);

        Task<User> Login(UserViewModel entity);

        Task<User> AddRoleToUser(int userId, string roleName);
        Task<User> RemoveRoleToUser(int userId, string roleName);
    }
}
