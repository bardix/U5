using BW2_Team6.Context;
using BW2_Team6.Models;
using BW2_Team6.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BW2_Team6.Services.Classes
{
	public class SellService : ISellService
	{
		private readonly DataContext _dbContext;
        private readonly ILogger<SellService> _logger;

        public SellService(DataContext dbtContext, ILogger<SellService> logger)
		{
			_dbContext = dbtContext;
            _logger = logger;
		}

        public async Task<Sell> Create(Sell newSell)
        {
            if (newSell == null)
            {
                throw new ArgumentNullException(nameof(newSell));
            }

            try
            { 
                newSell.DateSell = DateTime.Now;
                await _dbContext.Sells.AddAsync(newSell);
                await _dbContext.SaveChangesAsync();
                return newSell;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore nella creazione della vendita");
                throw;
            }
        }

        public async Task<Sell> Delete(int id)
        {
            try
            {
                var sell = await GetById(id);
                _dbContext.Sells.Remove(sell);
                await _dbContext.SaveChangesAsync();

                return sell;
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Vendita con ID {id} non trovato per eliminazione");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore nella eliminazione della vendita");
                throw;
            }
        }

        public async Task<List<Sell>> GetAll()
        {
            try
            {
                var sells = await _dbContext.Sells
                    .AsNoTracking()
                    .Include(s => s.Owner)
                    .Include(s => s.Product)
                    .ToListAsync();

                return sells;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore nel recupero di tutte le vendite");
                throw;
            }
        }

        public async Task<Sell> GetById(int id)
        {
            try
            {
                var sell = await _dbContext.Sells
                    .AsNoTracking()
                    .Include(s => s.Owner)
                    .Include(s => s.Product)
                    .FirstOrDefaultAsync(s => s.Id == id);
                if (sell == null)
                {
                    _logger.LogWarning($"Vendita con ID {id} non trovata");
                    throw new KeyNotFoundException($"Vendita non ID {id} non trovata");
                }

                return sell;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore nel recupero della vendita con ID {id}");
                throw;
            }
        }

        public async Task<Sell> Update(int id, Sell updateSell)
        {
            if (updateSell == null)
            {
                throw new ArgumentNullException(nameof(updateSell));
            }
            try
            {
                var sell = await GetById(id);

                sell.Owner = updateSell.Owner;
                sell.Product = updateSell.Product;
                sell.NumberOfRecipe = updateSell.NumberOfRecipe;

                _dbContext.Sells.Update(sell);
                await _dbContext.SaveChangesAsync();

                return sell;
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Vendita con ID {id} non trovata");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Errore nella modifica della vendita");
                throw;
            }
        }
    }
}

