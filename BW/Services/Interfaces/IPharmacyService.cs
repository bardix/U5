using BW2_Team6.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BW2_Team6.Services.Interfaces
{
    public interface IPharmacyService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<Product> CreateProduct(Product product);
        Task<Product> UpdateProduct(int id, Product product);
        Task<bool> DeleteProduct(int id);
        Task<IEnumerable<Sell>> GetSellsByCustomer(int id);
        Task<IEnumerable<Sell>> GetSellsByFiscalCode(string fiscalcode);

        Task<IEnumerable<Sell>> GetSellsByDate(DateTime data);

        Task<Product> SearchProduct(int id);

        Task<IEnumerable<Product>> GetFilteredProducts(string[] productTypes);

    }
}