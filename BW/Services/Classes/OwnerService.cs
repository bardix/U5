using BW2_Team6.Context;
using BW2_Team6.Models;
using BW2_Team6.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BW2_Team6.Services.Classes
{
    public class OwnerService : IOwnerService
    {
        private readonly DataContext _db;

        public OwnerService(DataContext db)
        {
            _db = db;
        }

        public async Task<Owner> Create(Owner owner)
        {
            await _db.Owners.AddAsync(owner);
            await _db.SaveChangesAsync();
            return owner;
        }

        public async Task<Owner> Update(int id, Owner updatedOwner)
        {
            var existingOwner = await _db.Owners.FindAsync(id);
            if (existingOwner == null) return null!;

            existingOwner.FirstName = updatedOwner.FirstName;
            existingOwner.LastName = updatedOwner.LastName;
            existingOwner.NumberPhone = updatedOwner.NumberPhone;
            existingOwner.Email = updatedOwner.Email;
            existingOwner.FiscalCode = updatedOwner.FiscalCode;
         

            await _db.SaveChangesAsync();
            return existingOwner;
        }

        public async Task<Owner> Delete(int id)
        {
            try
            {
                var owner = await Read(id);
                _db.Owners.Remove(owner);
                await _db.SaveChangesAsync();
                return owner;
            }
            catch (Exception ex)
            {
                throw new Exception("Owner not found", ex);
            }
        }

        public async Task<IEnumerable<Owner>> GetAll()
        {
            return await _db.Owners.ToListAsync();
        }

        public async Task<Owner> GetById(int id)
        {
            var owner = await _db.Owners.FirstOrDefaultAsync(o => o.Id == id);
            if (owner == null) return null!;
            return owner;
        }

        public async Task<Owner> Read(int id)
        {
            try
            {
                var owner = await _db.Owners.SingleOrDefaultAsync(i => i.Id == id);
                if (owner == null)
                {
                    throw new Exception("Owner not found");
                }
                return owner;
            }
            catch (Exception ex)
            {
                throw new Exception("Owner not found", ex);

            }
        }
    }
}
