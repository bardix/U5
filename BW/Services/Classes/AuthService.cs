using BW2_Team6.Context;
using BW2_Team6.Models;
using BW2_Team6.Services.Interfaces;
using BW2_Team6.Services.Password_Crypth_Implementations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace BW2_Team6.Services.Classes
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordEnconder _passwordEncoder;
        private readonly DataContext _db;

        public AuthService(IPasswordEnconder passwordEncoder, DataContext db)
        {
            _passwordEncoder = passwordEncoder;
            _db = db;
        }

        public async Task<User> AddRoleToUser(int userId, string roleName)
        {

            var user = await _db.Users.Include(u => u.Roles).SingleOrDefaultAsync(u => u.Id == userId);
            var role = await _db.Roles.SingleOrDefaultAsync(r => r.Name == roleName);
            if (user == null) { return null!; }
            if (role == null) { return null!; }
            if (!user.Roles.Contains(role))
            {
                user.Roles.Add(role);
            }
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User> Create(UserViewModel entity, IEnumerable<int> roleSelected)
        {
            var user = new User
            {
                Name = entity.Name,
                Email = entity.Email,
                Password = _passwordEncoder.Encode(entity.Password),
            };
            var roles = await _db.Roles.Where(r => roleSelected.Contains(r.Id)).ToListAsync();
            foreach (var role in roles)
            {
                user.Roles.Add(role);
            };

            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User> Delete(int id)
        {
            var user = await GetById(id);
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _db.Users
                    .AsNoTracking()
                    .Include(u => u.Roles)
                    .ToListAsync();
            return users;
        }

        public async Task<User> GetById(int id)
        {
            var user = await _db.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) { 
                return null!;
            }

            return user;
        }

        public async Task<User> Login(UserViewModel entity)
        {
            var user = await _db.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Email == entity.Email);
            if (user != null && _passwordEncoder.IsSame(entity.Password, user.Password))
            {
                var userResulted = new User
                {
                    Name = user.Name,
                    Email = user.Email,
                    Roles = user.Roles.ToList(),
                };
                return userResulted;

            }
            return null!;
        }
        // trovo l'user e il ruolo, se è presente tra i suoi ruoli lo rimuove
        public async Task<User> RemoveRoleToUser(int userId, string roleName)
        {
            var user = await _db.Users.Include(u => u.Roles).SingleOrDefaultAsync(u => u.Id == userId);
            var role = await _db.Roles.SingleOrDefaultAsync(r => r.Name == roleName);
            if(user == null) { return null!; }
            if(role == null) { return null!; }
            if (user.Roles.Contains(role))
            {
                user.Roles.Remove(role);
            }
            await _db.SaveChangesAsync();
            return user;
        }

    }
}
