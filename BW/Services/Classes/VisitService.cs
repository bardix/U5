using BW2_Team6.Context;
using BW2_Team6.Models;
using BW2_Team6.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BW2_Team6.Services.Classes
{
    public class VisitService : IVisitService
    {
            private readonly DataContext _db;

            public VisitService(DataContext db)
            {
                _db = db;
            }
        public async Task<Visit> Create(int animalId, VisitViewModel entity)
        { try
            {
                var animal = await _db.Animals.Include(a => a.Owner).SingleOrDefaultAsync(a => a.Id == animalId);
                if (animal == null)
                {
                    throw new Exception("L'animale non è stato trovato");
                }
                var visit = new Visit
                {
                    Animal = animal,
                    DateVisit = DateTime.Now,
                    TypeOfExam = entity.TypeOfExam,
                    TypeOfCure = entity.TypeOfCure
                };
                await _db.AddAsync(visit);
                await _db.SaveChangesAsync();
                return visit;
            }
            catch (Exception ex) 
                {
                    throw new Exception("Creation failed", ex);
                }
            }

        public async Task<Visit> Delete(int id)
        { try
            {
                var visit = await GetById(id);
                _db.Visits.Remove(visit);
                await _db.SaveChangesAsync();
                return visit;
            }
            catch (Exception ex) 
            {
                throw new Exception("Delete failed", ex);
            }

        }

        public async Task<IEnumerable<Visit>> GetAll()
        {
            var visits = await _db.Visits
                .AsNoTracking()
                .Include(v => v.Animal)
                .ToListAsync();
            return visits;
        }

        public async Task<Visit> GetById(int id)
        {
            var visit = await _db.Visits
                .AsNoTracking()
                .Include(v => v.Animal)
                .FirstOrDefaultAsync(v => v.Id == id);
            if (visit == null)
            {
                throw new Exception("Visit non found");
            }
            return visit;
        }

        public async Task<Visit> Update(int id, VisitViewModel entity)
        { try
            {
                var visit = await GetById(id);
                visit.TypeOfExam = entity.TypeOfExam;
                visit.TypeOfCure = entity.TypeOfCure;
                _db.Visits.Update(visit);
                await _db.SaveChangesAsync();
                return visit;

            }
            catch (Exception ex)
            {
                throw new Exception("Creation failed", ex);
            } 
        }

        public async Task<IEnumerable<Visit>> AllVisitByAnimalId(int animalId)
        {
            var visits = await _db.Visits
                .Include(v => v.Animal)
                .Where(v => v.Animal.Id == animalId)
                .OrderByDescending(v => v.DateVisit)
                .ToListAsync();
            return visits;

        }

    }
}
