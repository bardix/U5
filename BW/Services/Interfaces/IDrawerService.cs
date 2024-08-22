using BW2_Team6.Models;

namespace BW2_Team6.Services.Classes
{
    public interface IDrawerService
    {
        Task<IEnumerable<Drawer>> GetAll();
        
        Task<Drawer> Read(int id);
        Task<Drawer> Create(DrawerViewModel entity);

        Task<Drawer> Update(int id, DrawerViewModel entity);

        Task<Drawer> Delete(int id);
    }
}
