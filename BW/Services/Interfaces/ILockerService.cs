using BW2_Team6.Models;

namespace BW2_Team6.Services.Interfaces
{
    public interface ILockerService
    {
        Task<IEnumerable<LockerViewModel>> GetAll();

        Task<Locker> Create(LockerViewModel entity);
        Task<Locker> Update(int id, LockerViewModel entity);
       
        Task<Locker> Read(int id);

        Task<LockerViewModel> ReadV(int id);
        Task<Locker> Delete(int id);

        
    }
}
