using EsSettimanaleU5S3.DataModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ShopController : Controller
{
    private readonly PizzeriaDbContext _context;

    public ShopController(PizzeriaDbContext context)
    {
        _context = context;
    }

    // GET: Shop/Index
    public IActionResult Index()
    {
        var products = _context.Products.Include(p => p.IngredientProducts)
                                        .ThenInclude(ip => ip.Ingredient)
                                        .ToList();
        return View(products);
    }

    // GET: Shop/Details/5
    public IActionResult Details(int id)
    {
        var product = _context.Products.Include(p => p.IngredientProducts)
                                       .ThenInclude(ip => ip.Ingredient)
                                       .FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }
}
