using EsSettimanaleU5S3.DataModel;
using EsSettimanaleU5S3.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

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
                    // Crea le dichiarazioni (claims)
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Email, user.Email)
                    };

                    // Aggiungi i ruoli dell'utente alle dichiarazioni
                    var userRoles = user.RoleUsers.Select(ur => ur.Role.Name);
                    foreach (var role in userRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                    };

                    // Autentica l'utente
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Effettua il logout dell'utente
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Cancella i dati di TempData (se utilizzati)
            TempData["User"] = null;
            TempData["Roles"] = null;

            return RedirectToAction("Index", "Home");
        }

    }
}
