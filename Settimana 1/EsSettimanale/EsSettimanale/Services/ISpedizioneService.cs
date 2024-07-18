using EsSettimanale.Models;
using Microsoft.EntityFrameworkCore;

namespace EsSettimanale.Services
{
    public interface ISpedizioneService
    {
        Task<List<Spedizione>> GetAllSpedizioniAsync();
        Task<Spedizione> GetSpedizioneByIdAsync(int id);
        Task AddSpedizioneAsync(Spedizione spedizione);
        Task UpdateSpedizioneAsync(Spedizione spedizione);
        Task DeleteSpedizioneAsync(int id);
    }

    public class SpedizioneService : ISpedizioneService
    {
        private readonly ApplicationDbContext _context;

        public SpedizioneService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Spedizione>> GetAllSpedizioniAsync()
        {
            return await _context.Spedizioni.Include(s => s.Cliente).ToListAsync();
        }

        public async Task<Spedizione> GetSpedizioneByIdAsync(int id)
        {
            return await _context.Spedizioni.Include(s => s.Cliente).FirstOrDefaultAsync(s => s.ID == id);
        }

        public async Task AddSpedizioneAsync(Spedizione spedizione)
        {
            _context.Spedizioni.Add(spedizione);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSpedizioneAsync(Spedizione spedizione)
        {
            _context.Spedizioni.Update(spedizione);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSpedizioneAsync(int id)
        {
            var spedizione = await _context.Spedizioni.FindAsync(id);
            if (spedizione != null)
            {
                _context.Spedizioni.Remove(spedizione);
                await _context.SaveChangesAsync();
            }
        }
    }
}
