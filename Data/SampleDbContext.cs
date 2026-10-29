using Microsoft.EntityFrameworkCore;
using ProductosYCategorias.Models;

namespace ProductosYCategorias.Data;

public class SampleDbContext : DbContext
{
    public SampleDbContext(DbContextOptions<SampleDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Categories");

            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).UseIdentityColumn();

            entity.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(c => c.Description)
                .HasMaxLength(500);

            entity.HasIndex(c => c.Name)
                .IsUnique()
                .HasDatabaseName("UX_Categories_Name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");

            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).UseIdentityColumn();

            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(p => p.Price)
                .HasPrecision(18, 2);

            entity.Property(p => p.Stock)
                .IsRequired();

            entity.Property(p => p.FechaCreacion)
                .HasDefaultValueSql("SYSUTCDATETIME()");

            entity.HasIndex(p => p.Name)
                .HasDatabaseName("IX_Products_Name");

            entity.HasIndex(p => p.CategoryId)
                .HasDatabaseName("IX_Products_CategoryId");

            // Relación 1:N. Restrict evita borrar una categoría con productos.
            entity.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Products_Categories_CategoryId");
        });

        SeedData(modelBuilder);
    }

    /// <summary>
    /// Datos de demostración para que la paginación y los filtros sean visibles
    /// desde el primer arranque. Los identificadores y fechas son fijos para que
    /// las migraciones sean deterministas.
    /// </summary>
    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Tecnología", Description = "Dispositivos electrónicos y accesorios" },
            new Category { Id = 2, Name = "Hogar", Description = "Artículos para el hogar y decoración" },
            new Category { Id = 3, Name = "Deportes", Description = "Equipamiento deportivo y fitness" },
            new Category { Id = 4, Name = "Libros", Description = "Libros y material de lectura" },
            new Category { Id = 5, Name = "Alimentos", Description = "Productos alimenticios y bebidas" });

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Laptop Pro 14", Price = 1299.99m, Stock = 12, CategoryId = 1, FechaCreacion = new DateTime(2026, 1, 5, 9, 30, 0, DateTimeKind.Utc) },
            new Product { Id = 2, Name = "Mouse Inalámbrico", Price = 24.50m, Stock = 80, CategoryId = 1, FechaCreacion = new DateTime(2026, 1, 7, 11, 0, 0, DateTimeKind.Utc) },
            new Product { Id = 3, Name = "Teclado Mecánico", Price = 89.90m, Stock = 45, CategoryId = 1, FechaCreacion = new DateTime(2026, 1, 9, 15, 45, 0, DateTimeKind.Utc) },
            new Product { Id = 4, Name = "Monitor 27\" 4K", Price = 349.00m, Stock = 18, CategoryId = 1, FechaCreacion = new DateTime(2026, 1, 12, 8, 15, 0, DateTimeKind.Utc) },
            new Product { Id = 5, Name = "Auriculares Bluetooth", Price = 59.99m, Stock = 60, CategoryId = 1, FechaCreacion = new DateTime(2026, 1, 15, 17, 20, 0, DateTimeKind.Utc) },
            new Product { Id = 6, Name = "Lámpara de Escritorio", Price = 32.75m, Stock = 25, CategoryId = 2, FechaCreacion = new DateTime(2026, 1, 18, 10, 0, 0, DateTimeKind.Utc) },
            new Product { Id = 7, Name = "Juego de Sábanas", Price = 45.00m, Stock = 30, CategoryId = 2, FechaCreacion = new DateTime(2026, 1, 20, 12, 30, 0, DateTimeKind.Utc) },
            new Product { Id = 8, Name = "Cafetera de Goteo", Price = 79.90m, Stock = 15, CategoryId = 2, FechaCreacion = new DateTime(2026, 1, 23, 9, 10, 0, DateTimeKind.Utc) },
            new Product { Id = 9, Name = "Aspiradora Compacta", Price = 149.99m, Stock = 8, CategoryId = 2, FechaCreacion = new DateTime(2026, 1, 26, 14, 5, 0, DateTimeKind.Utc) },
            new Product { Id = 10, Name = "Set de Ollas", Price = 119.00m, Stock = 10, CategoryId = 2, FechaCreacion = new DateTime(2026, 1, 29, 16, 40, 0, DateTimeKind.Utc) },
            new Product { Id = 11, Name = "Balón de Fútbol", Price = 29.99m, Stock = 50, CategoryId = 3, FechaCreacion = new DateTime(2026, 2, 2, 8, 0, 0, DateTimeKind.Utc) },
            new Product { Id = 12, Name = "Mancuernas 10 kg", Price = 54.90m, Stock = 22, CategoryId = 3, FechaCreacion = new DateTime(2026, 2, 5, 11, 25, 0, DateTimeKind.Utc) },
            new Product { Id = 13, Name = "Esterilla de Yoga", Price = 19.99m, Stock = 70, CategoryId = 3, FechaCreacion = new DateTime(2026, 2, 8, 13, 50, 0, DateTimeKind.Utc) },
            new Product { Id = 14, Name = "Bicicleta Estática", Price = 299.00m, Stock = 6, CategoryId = 3, FechaCreacion = new DateTime(2026, 2, 11, 10, 35, 0, DateTimeKind.Utc) },
            new Product { Id = 15, Name = "Raqueta de Tenis", Price = 89.50m, Stock = 14, CategoryId = 3, FechaCreacion = new DateTime(2026, 2, 14, 15, 0, 0, DateTimeKind.Utc) },
            new Product { Id = 16, Name = "Cien Años de Soledad", Price = 21.90m, Stock = 40, CategoryId = 4, FechaCreacion = new DateTime(2026, 2, 17, 9, 45, 0, DateTimeKind.Utc) },
            new Product { Id = 17, Name = "El Quijote", Price = 18.50m, Stock = 35, CategoryId = 4, FechaCreacion = new DateTime(2026, 2, 20, 12, 0, 0, DateTimeKind.Utc) },
            new Product { Id = 18, Name = "Clean Code", Price = 39.99m, Stock = 20, CategoryId = 4, FechaCreacion = new DateTime(2026, 2, 23, 14, 30, 0, DateTimeKind.Utc) },
            new Product { Id = 19, Name = "Café Molido 500 g", Price = 8.75m, Stock = 100, CategoryId = 5, FechaCreacion = new DateTime(2026, 2, 26, 8, 20, 0, DateTimeKind.Utc) },
            new Product { Id = 20, Name = "Chocolate Negro 70%", Price = 4.20m, Stock = 120, CategoryId = 5, FechaCreacion = new DateTime(2026, 3, 1, 16, 10, 0, DateTimeKind.Utc) });
    }
}
