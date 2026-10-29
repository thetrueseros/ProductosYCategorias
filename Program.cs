using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using ProductosYCategorias.Data;

var builder = WebApplication.CreateBuilder(args);

// Servicios MVC.
builder.Services.AddControllersWithViews();

// Cadena de conexión: nunca en appsettings.json. En local se toma de
// appsettings.Development.json; en Azure, de la variable ConnectionStrings__DefaultConnection.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connectionString) ||
    connectionString.Contains("TU_SERVIDOR", StringComparison.OrdinalIgnoreCase))
{
    throw new InvalidOperationException(
        "Configura 'ConnectionStrings:DefaultConnection' en appsettings.Development.json " +
        "o define la variable de entorno 'ConnectionStrings__DefaultConnection'.");
}

builder.Services.AddDbContext<SampleDbContext>(options =>
    options.UseSqlServer(connectionString, sql =>
    {
        // Reintentos ante fallos transitorios típicos de Azure SQL.
        sql.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null);
        sql.CommandTimeout(60);
    }));

// Cultura única es-ES para formatos de moneda y fecha coherentes en local y en Azure.
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var culture = new CultureInfo("es-ES");
    options.DefaultRequestCulture = new RequestCulture(culture);
    options.SupportedCultures = new[] { culture };
    options.SupportedUICultures = new[] { culture };
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
app.UseHttpsRedirection();
app.UseRequestLocalization();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
