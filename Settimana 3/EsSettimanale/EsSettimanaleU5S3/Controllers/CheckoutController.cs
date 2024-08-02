using EsSettimanaleU5S3.DataModel;
using EsSettimanaleU5S3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using System;

public class CheckoutController : Controller
{
    private readonly CartService _cartService;
    private readonly PizzeriaDbContext _context;

    public CheckoutController(CartService cartService, PizzeriaDbContext context)
    {
        _cartService = cartService;
        _context = context;
    }

    public IActionResult Index()
    {
        // Assicurati di passare un nuovo ViewModel alla vista
        return View(new OrderViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(OrderViewModel model)
    {
        if (ModelState.IsValid)
        {
            var cart = _cartService.GetCart();
            if (!cart.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Ottieni l'ID dell'utente
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var order = new Order
            {
                UserId = int.Parse(userId), // Converte l'ID utente in int se necessario
                OrderDate = DateTime.Now,
                ShippingAddress = model.ShippingAddress,
                Notes = model.Notes,
                IsCompleted = false,
                OrderItems = cart.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    TotalPrice = item.Quantity * item.Product.Price
                }).ToList()
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            _cartService.ClearCart();

            return RedirectToAction("OrderConfirmed");
        }

        return View(model);
    }

    public IActionResult OrderConfirmed()
    {
        return View();
    }
}
