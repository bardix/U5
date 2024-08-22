using BW2_Team6.Models;
using BW2_Team6.Services.Classes;
using BW2_Team6.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace BW2_Team6.Controllers
{
    [Authorize(Policies.IsPharmacist)]
    public class DrawerController : Controller
    {
        private readonly IDrawerService _drawerSvc;
        private readonly IPharmacyService _pharmacySvc;
        private readonly ILockerService _lockerSvc;
        public DrawerController(IDrawerService drawerSvc, IPharmacyService pharmacySvc, ILockerService lockerSvc)
        {
            _drawerSvc = drawerSvc;
            _lockerSvc = lockerSvc;
            _pharmacySvc = pharmacySvc;
        }
        public async Task<IActionResult> AllDrawers()
        {
            var drawers = await _drawerSvc.GetAll();
            return View(drawers);
        }

        public async Task<IActionResult> Create()
        {
            var product = await _pharmacySvc.GetAllProducts();
            var lockers = await _lockerSvc.GetAll();
            ViewBag.Products = product;
            ViewBag.Lockers = lockers;
            return View();
        }
        [HttpPost]
         public async Task<IActionResult> Create(DrawerViewModel drawer)
        {  
            if(ModelState.IsValid)
            {
            await _drawerSvc.Create(drawer);
            return RedirectToAction("AllDrawers");
            }
            var product = await _pharmacySvc.GetAllProducts();
            var lockers = await _lockerSvc.GetAll();
            ViewBag.Products = product;
            ViewBag.Lockers = lockers;
            return View(drawer);
        }

        public async Task<IActionResult> Update(int id)
        {
            var product = await _pharmacySvc.GetAllProducts();
            var lockers = await _lockerSvc.GetAll();
            ViewBag.Products = product;
            ViewBag.Lockers = lockers;
            var drawer = await _drawerSvc.Read(id);
           // var productIds = drawer.Product.Product.Select(p => p.Id).ToList();
            var drawerV = new DrawerViewModel
            {
                LockerId = drawer.Locker.Id,
            //    productsId = productIds,
                
            };
            
            return View(drawerV);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, DrawerViewModel drawer)
        {
            
            if (ModelState.IsValid)
            {
               await _drawerSvc.Update(id, drawer);
                return RedirectToAction("AllDrawers");
            } 
                var product = await _pharmacySvc.GetAllProducts();
                var lockers = await _lockerSvc.GetAll();
                ViewBag.Products = product;
                ViewBag.Lockers = lockers;
                return View(drawer);
            
        }

        public async Task<IActionResult> Delete(int id)
        {
            var drawer = await _drawerSvc.Read(id);
            return View(drawer);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _drawerSvc.Delete(id);
            return RedirectToAction("AllDrawers");
        }

        public async Task<IActionResult> SearchProductInDrawer()
        {
            var products = await _pharmacySvc.GetAllProducts();
            return View(products);
        }
    }
}
