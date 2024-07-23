using System.Collections.Generic;
using System.Threading.Tasks;

namespace _1BW_BE.Service
{
    public interface IServizioPrenotazioneService
    {
        Task<IEnumerable<ServizioPrenotazione>> GetAllServiziPrenotazioniAsync();
        Task<ServizioPrenotazione> GetServizioPrenotazioneByIdAsync(int id);
        Task AddServizioPrenotazioneAsync(ServizioPrenotazione servizioPrenotazione);
        Task UpdateServizioPrenotazioneAsync(ServizioPrenotazione servizioPrenotazione);
        Task DeleteServizioPrenotazioneAsync(int id);
        Task<IEnumerable<ServizioPrenotazione>> GetServiziPrenotazioniByPrenotazioneIdAsync(int prenotazioneId);
    }
}