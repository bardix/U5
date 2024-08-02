using EsSettimanaleU5S3.DataModel;
using EsSettimanaleU5S3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EsSettimanaleU5S3.Controllers
{
    [Authorize]
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

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.Ingredients = _context.Ingredients.ToList();
            return View(new ProductViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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

                if (model.IngredientIds != null && model.IngredientIds.Any())
                {
                    foreach (var ingredientId in model.IngredientIds)
                    {
                        _context.IngredientProducts.Add(new IngredientProduct { ProductId = product.Id, IngredientId = ingredientId });
                    }
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            // Ricarica gli ingredienti se il modello non è valido
            ViewBag.Ingredients = _context.Ingredients.ToList();
            return View(model);
        }
    }
}
