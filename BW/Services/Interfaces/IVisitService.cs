using BW2_Team6.Models;

namespace BW2_Team6.Services.Interfaces
{
    public interface IVisitService
    {
        Task<IEnumerable<Visit>> GetAll();

        Task<Visit> GetById(int id);

        Task<Visit> Create(int animalId,VisitViewModel entity);
        Task<Visit> Update(int id,VisitViewModel entity);
        Task<Visit> Delete(int id);
        Task<IEnumerable<Visit>> AllVisitByAnimalId(int animalId);
    }
}
