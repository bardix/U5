using BW2_Team6.Models;

namespace BW2_Team6.Services.Interfaces
{
	public interface ISellService
	{
        public Task<Sell> Create(Sell newSell);
        public Task<List<Sell>> GetAll();
        public Task<Sell> GetById(int id);
        public Task<Sell> Update(int id, Sell updateSell);
        public Task<Sell> Delete(int id);
    }
}

