using EsercizioU5S2.Config;
using EsercizioU5S2.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
using _1BW_BE.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("DipendentePolicy", policy =>
        policy.RequireRole("Dipendente"));
});

// Bind the configuration section to the DatabaseConfig class
builder.Services.Configure<DatabaseConfig>(builder.Configuration.GetSection("ConnectionStrings"));

builder.Services.AddSingleton<SqlServerServiceBase>();
builder.Services.AddSingleton<IPrenotazioniService, PrenotazioniService>();
builder.Services.AddSingleton<IClientiService, ClientiService>();
builder.Services.AddSingleton<ICamereService, CamereService>();
builder.Services.AddSingleton<IServiziService, ServiziService>();
builder.Services.AddSingleton<IServizioPrenotazioneService, ServizioPrenotazioneService>();
builder.Services.AddSingleton<IUserService, UserService>();

// Register AuthService with the appropriate constructor parameter
builder.Services.AddSingleton<AuthService>(serviceProvider =>
{
    var config = serviceProvider.GetRequiredService<IOptions<DatabaseConfig>>().Value;
    return new AuthService(config.DefaultConnection);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
