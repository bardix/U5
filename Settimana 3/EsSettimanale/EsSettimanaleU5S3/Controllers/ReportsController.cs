using EsSettimanaleU5S3.DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EsSettimanaleU5S3.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ReportsController : Controller
    {
        private readonly PizzeriaDbContext _context;

        public ReportsController(PizzeriaDbContext context)
        {
            _context = context;
        }

        public IActionResult DailyReport(DateTime date)
        {
            var orders = _context.Orders
                .Where(o => o.OrderDate.Date == date.Date && o.IsCompleted)
                .ToList();

            var totalOrders = orders.Count;
            var totalRevenue = orders.Sum(o => o.OrderItems.Sum(oi => oi.TotalPrice));

            var model = new DailyReportViewModel
            {
                Date = date,
                TotalOrders = totalOrders,
                TotalRevenue = totalRevenue
            };

            return View(model);
        }
    }
}
