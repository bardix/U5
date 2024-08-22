using BW2_Team6.Models;

namespace BW2_Team6.Services.Interfaces
{
    public interface IRoleService
    {
        Task<Role> Create(Role entity);
        Task<Role> Update(Role entity);
        Task<Role> Delete(int id);

        Task<Role> Read(int id);

        Task<IEnumerable<Role>> GetAll();
    }
}
