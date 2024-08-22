using BW2_Team6.Context;
using BW2_Team6.Models;
using BW2_Team6.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BW2_Team6.Services.Classes
{
    public class CompaniesService : ICompaniesService
    {
        private readonly DataContext _db;

        public CompaniesService(DataContext db)
        {
            _db = db;
        }
        public async Task<Company> Create(Company entity)
        {
            await _db.Companies.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<Company> Delete(int id)
        {
            var company = await Read(id);
            _db.Remove(company);
            await _db.SaveChangesAsync();
            return company;
        }

        public async Task<IEnumerable<Company>> GetAll()
        {
            var companies = await _db.Companies.ToListAsync();
            return companies;
        }

        public async Task<Company> Read(int id)
        {
            var company = await _db.Companies.FirstOrDefaultAsync(x => x.Id == id);
            if (company == null) 
            {
                throw new Exception("Company not found");
            }
            return company;
        }

        public async Task<Company> Update(int id, Company entity)
        {
            var company = await Read(id);
            company.Name = entity.Name;
            company.Address = entity.Address;
            company.PhoneCompany = entity.PhoneCompany;
            _db.Companies.Update(company);
            await _db.SaveChangesAsync();
            return company;
        }
    }
}
