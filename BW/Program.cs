using BW2_Team6;
using BW2_Team6.Context;
using BW2_Team6.Services;
using BW2_Team6.Services.Classes;
using BW2_Team6.Services.Interfaces;
using BW2_Team6.Services.Password_Crypth_Implementations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt => opt.LoginPath = "/Auth/Login");

builder.Services.
              AddAuthorization(opt =>
              {
                  opt.AddPolicy(Policies.LoggedIn, cfg => cfg.RequireAuthenticatedUser());
                  opt.AddPolicy(Policies.IsAdmin, cfg => cfg.RequireRole("Admin"));
                  opt.AddPolicy(Policies.IsVeterinarian, cfg => cfg.RequireRole("Veterinario"));
                  opt.AddPolicy(Policies.IsPharmacist, cfg => cfg.RequireRole("Farmacista"));
              });
var conn = builder.Configuration.GetConnectionString("SqlServer")!;
builder.Services
    .AddDbContext<DataContext>(opt => opt.UseSqlServer(conn))
    ;

builder.Services
    .AddScoped<IRoleService, RoleService>()
    .AddScoped<IAuthService, AuthService>()
    .AddScoped<IPasswordEnconder, PassowordEnconder>()
    .AddScoped<IOwnerService, OwnerService>()
    .AddScoped<IAnimalService, AnimalService>()
    .AddScoped<IVisitService, VisitService>()
    .AddScoped<IRecoverService, RecoverService>()
    .AddScoped<ICompaniesService, CompaniesService>()
    .AddScoped<IPharmacyService, PharmacyService>()
    .AddScoped<ISellService, SellService>()
    .AddScoped<ILockerService, LockerService>()
    .AddScoped<IDrawerService, DrawerService>()
    ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Recover}/{action=SearchYourAnimal}/{id?}");

app.Run();
