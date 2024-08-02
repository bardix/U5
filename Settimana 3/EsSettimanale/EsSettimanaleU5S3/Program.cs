using EsSettimanaleU5S3.DataModel;
 // Assicurati di aggiungere il namespace corretto
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Aggiungi i servizi al container
builder.Services.AddControllersWithViews();

// Configura l'autenticazione con cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

// Configura il DbContext
builder.Services.AddDbContext<PizzeriaDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

// Aggiungi i servizi per la sessione
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Registra IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Registra CartService
builder.Services.AddScoped<CartService>();

// Costruisci l'app
var app = builder.Build();

// Configura la pipeline delle richieste HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Usa autenticazione e autorizzazione
app.UseAuthentication();
app.UseAuthorization();

// Usa la sessione
app.UseSession();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
