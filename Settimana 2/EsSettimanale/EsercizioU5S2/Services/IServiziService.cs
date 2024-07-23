using System.Collections.Generic;
using System.Threading.Tasks;

namespace _1BW_BE.Service
{
    public interface IServiziService
    {
        Task<IEnumerable<Servizio>> GetAllServiziAsync();
        Task<Servizio> GetServizioByIdAsync(int id);
        Task AddServizioAsync(Servizio servizio);
        Task UpdateServizioAsync(Servizio servizio);
        Task DeleteServizioAsync(int id);
    }
}
