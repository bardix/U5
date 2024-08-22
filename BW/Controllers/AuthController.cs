using BW2_Team6.Models;
using BW2_Team6.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BW2_Team6.Controllers
{
    [Authorize(Policies.IsAdmin)]
    public class AuthController : Controller
    {
        
            private readonly IRoleService _roleSvc;
            private readonly IAuthService _authSvc;

            public AuthController(IRoleService roleSvc, IAuthService authSvc)
            {
                _roleSvc = roleSvc;
                _authSvc = authSvc;
            }

            public async Task<IActionResult> AllRoles()
            {
                var roles = await _roleSvc.GetAll();
                return View(roles);
            }

            public IActionResult CreateRole()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> CreateRole(Role role)
            {
                if (ModelState.IsValid)
                {
                    await _roleSvc.Create(role);
                    return RedirectToAction("AllRoles", "Auth");
                }
                return View(role);
            }

            public async Task<IActionResult> EditRole(int id)
            {
                var role = await _roleSvc.Read(id);
                return View(role);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> EditRole(Role role)
            {
                if (ModelState.IsValid)
                {
                    await _roleSvc.Update(role);
                    return RedirectToAction("AllRoles", "Auth");
                }
                return View(role);
            }

            public async Task<IActionResult> DeleteRole(int id)
            {
                var role = await _roleSvc.Read(id);
                return View(role);
            }

            public async Task<IActionResult> DeleteConfirmedRole(int id)
            {
                await _roleSvc.Delete(id);
                return RedirectToAction("AllRoles", "Auth");
            }

            [AllowAnonymous]
            public async Task<IActionResult> Register()
            {
            var roles = await _roleSvc.GetAll();
            ViewBag.Roles = roles;
            return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            [AllowAnonymous]
            public async Task<IActionResult> Register(UserViewModel user, IEnumerable<int> roleSelected)
            {   
                
                if (ModelState.IsValid)
                {
                    await _authSvc.Create(user, roleSelected);
                    return RedirectToAction("Login", "Auth");
                }
                return View();
            }
            [AllowAnonymous]
            public IActionResult Login()
            {
                return View();
            }


            public async Task<IActionResult> AllUsers()
            {
                var users = await _authSvc.GetAll();
                return View(users);
            }

            public async Task<IActionResult> DeleteUser(int id)
            {
                var user = await _authSvc.GetById(id);
                return View(user);
            }


            public async Task<IActionResult> DeleteUserConfirmed(int id)
            {
                await _authSvc.Delete(id);
                return RedirectToAction("AllUsers", "Auth");
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            [AllowAnonymous]
            public async Task<IActionResult> Login(UserViewModel user)
            {
                var u = await _authSvc.Login(user);
                if (u != null)
                {
                    var claims = new List<Claim>
                     {
                     new Claim(ClaimTypes.Name, u.Name),
                     };
                    u.Roles.ForEach(r => claims.Add(new Claim(ClaimTypes.Role, r.Name)));

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);


                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity));
                }
                return RedirectToAction("Index", "Home");
            }

            [AllowAnonymous]
            public async Task<IActionResult> Logout()
            {
                await HttpContext.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }

            public async Task<IActionResult> DetailUser(int id)
            {
                // trovo l'user
                var user = await _authSvc.GetById(id);
                // mappo i suoi ruoli
                var userRoles = user.Roles.Select(r => r.Name).ToList();
                // tutti i ruoli
                var allRoles = await _roleSvc.GetAll();
                // mappo i nomi dei ruoli 
                var rolesName = allRoles.Select(role => role.Name).ToList();
                // prendo i ruoli da rimuovere
                var rolesToRemove = rolesName.Where(r => userRoles.Contains(r)).ToList();

                // rimuovo i ruoli
                foreach (var item in rolesToRemove)
                {
                    rolesName.Remove(item);
                }
                ViewBag.Roles = rolesName;
                return View(user);
            }

            public async Task<IActionResult> AddRoleToUser(int userid, string roleName)
            {   
                
                await _authSvc.AddRoleToUser(userid, roleName);
                return RedirectToAction("AllUsers", "Auth");
            }
            public async Task<IActionResult> RemoveRoleToUser(int userid, string roleName)
            {
                await _authSvc.RemoveRoleToUser(userid, roleName);
                return RedirectToAction("AllUsers", "Auth");
            }
        }
    }

