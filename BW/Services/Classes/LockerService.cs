using BW2_Team6.Context;
using BW2_Team6.Models;
using BW2_Team6.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BW2_Team6.Services.Classes
{
    public class LockerService : ILockerService
    {
        private readonly DataContext _db;

        public LockerService(DataContext db)
        {
            _db = db;
        }
        public async Task<Locker> Create(LockerViewModel entity)
        {
            var locker = new Locker
            {
                NumberLocker = entity.NumberLocker,
            };
            await _db.AddAsync(locker);
            await _db.SaveChangesAsync();
            return locker;
        }

        public async Task<Locker> Delete(int id)
        {
            var locker = await Read(id);
            _db.Locker.Remove(locker);
            await _db.SaveChangesAsync();
            return locker;
        }

        public async Task<IEnumerable<LockerViewModel>> GetAll()
        {
               var lockers = await _db.Locker
                .ToListAsync();
            var lockerV = new List<LockerViewModel>();

            foreach(var locker in lockers)
            {
                lockerV.Add(new LockerViewModel
                { Id = locker.Id,
                    NumberLocker = locker.NumberLocker,
                });
            }
            foreach(var locker in lockerV) {
                locker.Drawer = await _db.Drawers.Include(x => x.Locker)
                    .Where(x => x.Locker.Id == locker.Id)
                    .ToListAsync();
            }
            return lockerV;
        }

        public async Task<Locker> Read(int id)
        {
            var locker = await _db.Locker.FirstOrDefaultAsync(locker => locker.Id == id);
            if (locker == null) 
            {
                throw new Exception("Locker not found");
            }
            return locker;
        }

        public async Task<LockerViewModel> ReadV(int id)
        {
            var locker = await _db.Locker.FirstOrDefaultAsync(locker => locker.Id == id);
            if (locker == null)
            {
                throw new Exception("Locker not found");
            }
            var lockerV = new LockerViewModel
            {
                Id = locker.Id,
                NumberLocker = locker.NumberLocker,

            };

            lockerV.Drawer = await _db.Drawers.Include(x => x.Locker)
                    .Where(x => x.Locker.Id == locker.Id)
                    .ToListAsync();
            return lockerV;
        }

        public async Task<Locker> Update(int id, LockerViewModel entity)
        {
            var locker = await Read(id);
            locker.NumberLocker = entity.NumberLocker;
            _db.Locker.Update(locker);
            await _db.SaveChangesAsync();
            return locker;
        }

    }
}
