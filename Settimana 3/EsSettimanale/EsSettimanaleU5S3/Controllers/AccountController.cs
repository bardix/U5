using EsSettimanaleU5S3.DataModel;
using EsSettimanaleU5S3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EsSettimanaleU5S3.Controllers
{
    public class AccountController : Controller
    {
        private readonly PizzeriaDbContext _context;

        public AccountController(PizzeriaDbContext context)
        {
            _context = context;
        }

        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users
                    .Include(u => u.RoleUsers)
                    .ThenInclude(ru => ru.Role)
                   .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

                if (user != null)
                {
                    TempData["User"] = user.Name;
                    var roles = user.RoleUsers.Select(ur => ur.Role.Name).ToArray();
                    TempData["Roles"] = roles;
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Tentativo di accesso non valido.");
            }
            return View(model);
        }

        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password // In un'applicazione reale, la password dovrebbe essere hashata
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var role = await _context.Roles.SingleOrDefaultAsync(r => r.Name == model.Role);
                if (role != null)
                {
                    _context.RoleUsers.Add(new RoleUser { RoleId = role.Id, UserId = user.Id });
                    await _context.SaveChangesAsync();
                }

                TempData["User"] = user.Name;
                TempData["Roles"] = new string[] { model.Role };
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        // POST: Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            TempData["User"] = null;
            TempData["Roles"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}
