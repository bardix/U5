using BW2_Team6.Context;
using BW2_Team6.Models;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;

namespace BW2_Team6.Services.Classes
{
    public class DrawerService : IDrawerService
    {
        private readonly DataContext _db;

        public DrawerService(DataContext db)
        {
            _db = db;
        }
        public async Task<Drawer> Create(DrawerViewModel entity)
        {
            var locker = await _db.Locker.FirstOrDefaultAsync(l => l.Id == entity.LockerId);
            if (locker == null)
            {
                throw new Exception("locker not found");
            }

            var products = new List<DrawerProduct>();  
            foreach (var id in entity.productsId) {
                var product = _db.Products.FirstOrDefault(p => p.Id == id);
                if (product == null) {
                    throw new Exception("product not found");
                }
                products.Add(new DrawerProduct { Product = product });
            }

            var drawer = new Drawer();

            drawer.Locker = locker;
            drawer.Product = products;

            await _db.Drawers.AddAsync(drawer);
            await _db.SaveChangesAsync();
            return drawer;
        }

        public async Task<Drawer> Delete(int id)
        {
            var drawer = await Read(id);
            _db.Drawers.Remove(drawer);
            await _db.SaveChangesAsync();
            return drawer;
        }

        public async Task<IEnumerable<Drawer>> GetAll()
        {
            var drawers = await _db.Drawers
                .Include(d => d.Locker)
                .Include(d => d.Product)
                .ThenInclude(dp => dp.Product)
                .ToListAsync();
            return drawers;
        }



        public async Task<Drawer> Read(int id)
        {
            var drawer = await _db.Drawers
                .Include(d => d.Locker)
                .Include(d => d.Product)
                .FirstOrDefaultAsync(d => d.Id == id);
            if (drawer == null) {
                throw new Exception("Drawer not found");
            }
            return drawer;
        }

        public async Task<Drawer> Update(int id, DrawerViewModel entity)
        {
            var drawer = await Read(id);
            var locker = await _db.Locker.FirstOrDefaultAsync(l => l.Id == entity.LockerId);
            if (locker == null)
            {
                throw new Exception("locker not found");
            }

            var products = new List<DrawerProduct>();
            foreach (var singleproductId in entity.productsId)
            {
                var product = _db.Products.FirstOrDefault(p => p.Id == singleproductId);
                if (product == null)
                {
                    throw new Exception("product not found");
                }
                products.Add(new DrawerProduct { Product = product });
            }

            

            drawer.Locker = locker;
            drawer.Product = products;

            _db.Drawers.Update(drawer);
            await _db.SaveChangesAsync();
            return drawer;

        }

       
    }
}
