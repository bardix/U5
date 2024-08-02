using EsSettimanaleU5S3.DataModel;
using EsSettimanaleU5S3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EsSettimanaleU5S3.Controllers
{
    [Authorize(Roles = "User, Admin")]
    public class CartController : Controller
    {
        private readonly PizzeriaDbContext _context;
        private readonly CartService _cartService;

        public CartController(PizzeriaDbContext context, CartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        // Visualizza il carrello
        public IActionResult Index()
        {
            var cartItems = _cartService.GetCart();
            return View(cartItems);
        }

        // Aggiungi un prodotto al carrello
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int productId, int quantity)
        {
            _cartService.AddToCart(productId, quantity);
            return RedirectToAction("Index");
        }

        // Rimuovi un prodotto dal carrello
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(int productId)
        {
            _cartService.RemoveFromCart(productId);
            return RedirectToAction("Index");
        }

        // Visualizza la pagina di checkout
        public IActionResult Checkout()
        {
            var cartItems = _cartService.GetCart();
            return View(new CheckoutViewModel { CartItems = cartItems });
        }

        // Conferma l'ordine
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            if (ModelState.IsValid)
            {
                var cartItems = _cartService.GetCart();
                var userId = User.FindFirst("UserId")?.Value;

                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                var order = new Order
                {
                    UserId = int.Parse(userId),
                    OrderDate = DateTime.Now,
                    ShippingAddress = model.ShippingAddress,
                    Notes = model.Notes,
                    IsCompleted = false,
                    OrderItems = cartItems.Select(ci => new OrderItem
                    {
                        ProductId = ci.ProductId,
                        Quantity = ci.Quantity,
                        TotalPrice = ci.Product.Price * ci.Quantity
                    }).ToList()
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                _cartService.ClearCart();
                return RedirectToAction("Summary", new { id = order.Id });
            }

            return View(model);
        }

        // Visualizza il riepilogo dell'ordine
        public IActionResult OrderSummary(int id)
        {
            var order = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
                return NotFound();

            return View(order);
        }
    }
}
