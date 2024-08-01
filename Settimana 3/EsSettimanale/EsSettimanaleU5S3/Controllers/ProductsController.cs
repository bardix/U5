using EsSettimanaleU5S3.DataModel;
using EsSettimanaleU5S3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EsSettimanaleU5S3.Controllers
{
 
    public class ProductsController : Controller
    {
        private readonly PizzeriaDbContext _context;

        public ProductsController(PizzeriaDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Ingredients = _context.Ingredients.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    DeliveryTime = model.DeliveryTime,
                    PhotoUrl = model.PhotoUrl
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                if (model.IngredientIds != null)
                {
                    foreach (var ingredientId in model.IngredientIds)
                    {
                        _context.IngredientProducts.Add(new IngredientProduct { ProductId = product.Id, IngredientId = ingredientId });
                    }
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = _context.Products.Find(model.Id);
                if (product != null)
                {
                    product.Name = model.Name;
                    product.Price = model.Price;
                    product.DeliveryTime = model.DeliveryTime;
                    product.PhotoUrl = model.PhotoUrl;

                    _context.Products.Update(product);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
