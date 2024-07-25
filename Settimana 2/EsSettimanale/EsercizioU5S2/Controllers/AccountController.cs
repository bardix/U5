using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EsercizioU5S2.Models;
using EsercizioU5S2.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

public class AccountController : Controller
{
    private readonly IAuthService _authService;

    public AccountController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var user = await _authService.AuthenticateUserAsync(username, password);

        if (user != null)
        {
            await _authService.SignInAsync(HttpContext, user);
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Message = "Username o password non validi";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Account");
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }
}
