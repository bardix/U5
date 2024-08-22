using BW2_Team6.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BW2_Team6.Services.Interfaces
{
    public interface IOwnerService
    {
        Task<Owner> Create(Owner owner);
        Task<Owner> Update(int id, Owner updatedOwner);
        Task<Owner> Delete(int id);
        Task<IEnumerable<Owner>> GetAll();

        Task<Owner> Read(int id);
    }
}
