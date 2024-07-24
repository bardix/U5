﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EsercizioU5S2.Models; // Sostituisci con lo spazio dei nomi corretto
using EsercizioU5S2.Services; // Sostituisci con lo spazio dei nomi corretto
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

public class AccountController : Controller
{
    private readonly AuthService _authService;

    public AccountController(AuthService authService)
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
        // Simula l'hash della password, in produzione usa un metodo sicuro per hash
        string passwordHash = password;

        User user = await _authService.AuthenticateUserAsync(username, passwordHash);

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
