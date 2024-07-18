using EsSettimanale.Services;

var builder = WebApplication.CreateBuilder(args);

// Registrazione dei servizi
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<ISpedizioneService, SpedizioneService>();
builder.Services.AddScoped<IAggiornamentoSpedizioneService, AggiornamentoSpedizioneService>();

// Aggiunta di Razor Pages e MVC
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();