using EsSettimanaleU5S3.DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EsSettimanaleU5S3.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class IngredientsController : Controller
    {
        private readonly PizzeriaDbContext _context;

        public IngredientsController(PizzeriaDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var ingredients = _context.Ingredients.ToList();
            return View(ingredients);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                _context.Ingredients.Add(ingredient);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }

        public IActionResult Edit(int id)
        {
            var ingredient = _context.Ingredients.Find(id);
            if (ingredient == null) return NotFound();

            return View(ingredient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                _context.Ingredients.Update(ingredient);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }

        public IActionResult Delete(int id)
        {
            var ingredient = _context.Ingredients.Find(id);
            if (ingredient == null) return NotFound();

            return View(ingredient);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var ingredient = _context.Ingredients.Find(id);
            if (ingredient != null)
            {
                _context.Ingredients.Remove(ingredient);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
