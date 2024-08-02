using EsSettimanaleU5S3.DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EsSettimanaleU5S3.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly PizzeriaDbContext _context;

        public OrdersController(PizzeriaDbContext context)
        {
            _context = context;
        }

        // Azione per mostrare tutti gli ordini
        public IActionResult Index()
        {
            var orders = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToList(); // Assicurati di includere gli articoli dell'ordine e i prodotti collegati
            return View(orders);
        }

        // Azione per creare un nuovo ordine
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ConfirmOrder), new { orderId = order.Id });
            }
            return View(order);
        }

        // Azione per aggiungere un prodotto all'ordine
        [HttpPost]
        public IActionResult AddProduct(int productId, int quantity)
        {
            var product = _context.Products.Find(productId);
            if (product != null)
            {
                var orderItem = new OrderItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    TotalPrice = product.Price * quantity
                };
                _context.OrderItems.Add(orderItem);
                _context.SaveChanges();
            }
            return Json(new { success = true });
        }

        // Azione per confermare un ordine
        public IActionResult ConfirmOrder(int orderId)
        {
            var order = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .SingleOrDefault(o => o.Id == orderId);
            if (order == null) return NotFound();

            return View(order);
        }

        // Azione per segnare un ordine come spedito (riservato agli admin)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult MarkAsShipped(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                order.IsCompleted = true;
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        // Azione per mostrare il riepilogo di un ordine
        public IActionResult Summary(int orderId)
        {
            var order = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.Id == orderId);

            if (order == null) return NotFound();

            return View(order);
        }
    }
}
