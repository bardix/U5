using BW2_Team6.Models;

namespace BW2_Team6.Services.Interfaces
{
    public interface IRecoverService
    {

        Task<Recover> Create(int animalId, RecoverViewModel entity);
        Task<Recover> Update(int id, RecoverViewModel entity);

        Task<Recover> Delete(int id);

        Task<Recover> Read(int id);

        Task<IEnumerable<Recover>> GetAll();

        Task<IEnumerable<Recover>> GetAllActive();

        Task<Recover> SearchAnimalByMicrochip(string microchip);

        Task<Recover> ChangeStatus(int id);
        
    }
}
