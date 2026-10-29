using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ProductosYCategorias.Data;

/// <summary>
/// Fábrica usada exclusivamente por las herramientas de EF Core (<c>dotnet ef</c>).
/// Permite generar migraciones sin arrancar la aplicación ni necesitar una base real.
/// </summary>
public class SampleDbContextFactory : IDesignTimeDbContextFactory<SampleDbContext>
{
    // Cadena de conexión de conveniencia para design-time: no se conecta al generar migraciones.
    private const string FallbackConnectionString =
        "Server=localhost,1433;Database=ProductosYCategorias;User Id=sa;Password=Local_dev_only_123!;TrustServerCertificate=True";

    public SampleDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            connectionString = FallbackConnectionString;
        }

        var options = new DbContextOptionsBuilder<SampleDbContext>()
            .UseSqlServer(connectionString, sql => sql.EnableRetryOnFailure())
            .Options;

        return new SampleDbContext(options);
    }
}
