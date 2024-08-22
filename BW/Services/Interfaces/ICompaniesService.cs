using BW2_Team6.Models;

namespace BW2_Team6.Services.Interfaces
{
    public interface ICompaniesService
    {
        Task<IEnumerable<Company>> GetAll();

        Task<Company> Create(Company entity);
        Task<Company> Update(int id, Company entity);

        Task<Company> Read(int id);
        Task<Company> Delete(int id);

        
    }
}
