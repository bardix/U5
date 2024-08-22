using BW2_Team6.Context;
using BW2_Team6.Models;
using BW2_Team6.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BW2_Team6.Services.Classes
{
    public class RoleService : IRoleService
    {
        private readonly DataContext _db;

        public RoleService(DataContext db)
        {
            _db = db;
        }
        public async Task<Role> Create(Role entity)
        {
            try
            {
                await _db.Roles.AddAsync(entity);
                await _db.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Role not created", ex);
            }
        }

        public async Task<Role> Delete(int id)
        {
            try
            {
                var role = await Read(id);
                _db.Roles.Remove(role);
                await _db.SaveChangesAsync();
                return role;
            }
            catch (Exception ex)
            {
                throw new Exception("Role not found", ex);
            }
        }

        public async Task<Role> Read(int id)
        {
            try
            {
                var role = await _db.Roles.SingleOrDefaultAsync(i => i.Id == id);
                if (role == null) {
                    throw new Exception("Role not found");
                }
                return role;
            }
            catch (Exception ex)
            {
                throw new Exception("Role not found", ex);

            }
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            try
            {
                return await _db.Roles.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Role not found", ex);
            }
        }

        public async Task<Role> Update(Role entity)
        {
            try
            {
                var role = await Read(entity.Id);
                role.Name = entity.Name;
                _db.Update(role);
                await _db.SaveChangesAsync();
                return role;
            }
            catch (Exception ex)
            {
                throw new Exception("Role not updated", ex);
            }
        }
    }
}

