using EsSettimanale.Models;
using Microsoft.EntityFrameworkCore;

namespace EsSettimanale.Services
{
    public interface IAggiornamentoSpedizioneService
    {
        Task<List<AggiornamentoSpedizione>> GetAllAggiornamentiBySpedizioneIdAsync(int spedizioneId);
        Task AddAggiornamentoSpedizioneAsync(AggiornamentoSpedizione aggiornamento);
    }

    public class AggiornamentoSpedizioneService : IAggiornamentoSpedizioneService
    {
        private readonly ApplicationDbContext _context;

        public AggiornamentoSpedizioneService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AggiornamentoSpedizione>> GetAllAggiornamentiBySpedizioneIdAsync(int spedizioneId)
        {
            return await _context.AggiornamentiSpedizioni
                .Where(a => a.SpedizioneID == spedizioneId)
                .OrderByDescending(a => a.DataOraAggiornamento)
                .ToListAsync();
        }

        public async Task AddAggiornamentoSpedizioneAsync(AggiornamentoSpedizione aggiornamento)
        {
            _context.AggiornamentiSpedizioni.Add(aggiornamento);
            await _context.SaveChangesAsync();
        }
    }
}
