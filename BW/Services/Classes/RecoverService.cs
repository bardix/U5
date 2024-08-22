using BW2_Team6.Context;
using BW2_Team6.Models;
using BW2_Team6.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BW2_Team6.Services.Classes
{
    public class RecoverService : IRecoverService
    {

        private readonly DataContext _db;

        public RecoverService(DataContext db)
        {
            _db = db;
        }
        private string ConvertImage(IFormFile image)
        {

            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.CopyTo(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();
                string base64String = Convert.ToBase64String(fileBytes);
                string urlImg = $"data:image/jpeg;base64,{base64String}";
                return urlImg;
            }
        }
        public async Task<Recover> Create(int animalId, RecoverViewModel entity)
        { try
            {
                var animal = await _db.Animals.Include(a => a.Owner).SingleOrDefaultAsync(a => a.Id == animalId);
                if (animal == null)
                {
                    throw new Exception("L'animale non è stato trovato");
                }
                var recover = new Recover
                {
                    Animal = animal,
                    DateRecover = DateTime.Now,
                    Image = ConvertImage(entity.Image),
                    IsActive = entity.IsActive,
                };
                await _db.Recovers.AddAsync(recover);
                await _db.SaveChangesAsync();
                return recover;
            }
            catch (Exception ex) {
                throw new Exception("Create failed",ex);
            }
        }

        public async Task<Recover> Delete(int id)
        {
            var recover = await Read(id);
            _db.Recovers.Remove(recover);
            await _db.SaveChangesAsync();
            return recover;
        }

        public async Task<IEnumerable<Recover>> GetAll()
        {
            var recovers = await _db.Recovers
                .AsNoTracking()
                .Include(r => r.Animal)
                .ToListAsync();
            return recovers;
        }
        public async Task<IEnumerable<Recover>> GetAllActive()
        {
            var recovers = await _db.Recovers
                .AsNoTracking()
                .Include(r => r.Animal)
                .Where(r => r.IsActive)
                .ToListAsync();
            return recovers;
        }

        public async Task<Recover> Read(int id)
        {
           var recover = await _db.Recovers.AsNoTracking().Include(r => r.Animal).FirstOrDefaultAsync(r => r.Id == id);
            if (recover == null)
            {
                throw new Exception("Recover not found");
            }
            return recover;

        }

        public async Task<Recover> Update(int id, RecoverViewModel entity)
        {
            var recover = await Read(id);
            recover.Image = ConvertImage(entity.Image);
            recover.IsActive = entity.IsActive;
            _db.Recovers.Update(recover);
            await _db.SaveChangesAsync();
            return recover;
        }

        public async Task<Recover> SearchAnimalByMicrochip(string microchip)
        {
            var recover = await _db.Recovers
                .AsNoTracking()
                .Include(r => r.Animal).FirstOrDefaultAsync(r => r.Animal.Microchip == microchip);
            if (recover == null)
            {
                throw new Exception("Recover not found");
            }
            return recover;
        }

        public async Task<Recover> ChangeStatus(int id)
        {
            var recover = await Read(id);

            recover.IsActive = !recover.IsActive;
            _db.Recovers.Update(recover);
            await _db.SaveChangesAsync();
            return recover;
        }
    }
}
