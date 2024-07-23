using System.Collections.Generic;
using System.Threading.Tasks;

namespace _1BW_BE.Service
{
    public interface IPrenotazioniService
    {
        Task<IEnumerable<Prenotazione>> GetAllPrenotazioniAsync();
        Task<Prenotazione> GetPrenotazioneByIdAsync(int id);
        Task AddPrenotazioneAsync(Prenotazione prenotazione);
        Task UpdatePrenotazioneAsync(Prenotazione prenotazione);
        Task DeletePrenotazioneAsync(int id);
        Task<bool> PrenotazioneExistsAsync(int id);
        Task<IEnumerable<Prenotazione>> GetPrenotazioniByClienteCodiceFiscaleAsync(string codiceFiscale);
        Task<int> GetNumeroPrenotazioniPensioneCompletaAsync();
    }
}
