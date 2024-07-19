using EsVenerdi.Services;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Aggiungi i servizi al contenitore.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<AnagraficaService>();
builder.Services.AddSingleton<TipoViolazioneService>();
builder.Services.AddSingleton<VerbaleService>();

var app = builder.Build();

// Configura la pipeline HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();