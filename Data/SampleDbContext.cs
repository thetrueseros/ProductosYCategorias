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
            new Product { Id = 1, Name = "Laptop Pro 14", Price = 5499900m, Stock = 12, CategoryId = 1, FechaCreacion = new DateTime(2026, 1, 5, 14, 30, 0, DateTimeKind.Utc) },
            new Product { Id = 2, Name = "Mouse Inalámbrico", Price = 89900m, Stock = 80, CategoryId = 1, FechaCreacion = new DateTime(2026, 1, 7, 16, 0, 0, DateTimeKind.Utc) },
            new Product { Id = 3, Name = "Teclado Mecánico", Price = 249900m, Stock = 45, CategoryId = 1, FechaCreacion = new DateTime(2026, 1, 9, 20, 45, 0, DateTimeKind.Utc) },
            new Product { Id = 4, Name = "Monitor 27\" 4K", Price = 1499900m, Stock = 18, CategoryId = 1, FechaCreacion = new DateTime(2026, 1, 12, 13, 15, 0, DateTimeKind.Utc) },
            new Product { Id = 5, Name = "Auriculares Bluetooth", Price = 199900m, Stock = 60, CategoryId = 1, FechaCreacion = new DateTime(2026, 1, 15, 22, 20, 0, DateTimeKind.Utc) },
            new Product { Id = 6, Name = "Lámpara de Escritorio", Price = 79900m, Stock = 25, CategoryId = 2, FechaCreacion = new DateTime(2026, 1, 18, 15, 0, 0, DateTimeKind.Utc) },
            new Product { Id = 7, Name = "Juego de Sábanas", Price = 129900m, Stock = 30, CategoryId = 2, FechaCreacion = new DateTime(2026, 1, 20, 17, 30, 0, DateTimeKind.Utc) },
            new Product { Id = 8, Name = "Cafetera de Goteo", Price = 189900m, Stock = 15, CategoryId = 2, FechaCreacion = new DateTime(2026, 1, 23, 14, 10, 0, DateTimeKind.Utc) },
            new Product { Id = 9, Name = "Aspiradora Compacta", Price = 599900m, Stock = 8, CategoryId = 2, FechaCreacion = new DateTime(2026, 1, 26, 19, 5, 0, DateTimeKind.Utc) },
            new Product { Id = 10, Name = "Set de Ollas", Price = 349900m, Stock = 10, CategoryId = 2, FechaCreacion = new DateTime(2026, 1, 29, 21, 40, 0, DateTimeKind.Utc) },
            new Product { Id = 11, Name = "Balón de Fútbol", Price = 89900m, Stock = 50, CategoryId = 3, FechaCreacion = new DateTime(2026, 2, 2, 13, 0, 0, DateTimeKind.Utc) },
            new Product { Id = 12, Name = "Mancuernas 10 kg", Price = 149900m, Stock = 22, CategoryId = 3, FechaCreacion = new DateTime(2026, 2, 5, 16, 25, 0, DateTimeKind.Utc) },
            new Product { Id = 13, Name = "Esterilla de Yoga", Price = 59900m, Stock = 70, CategoryId = 3, FechaCreacion = new DateTime(2026, 2, 8, 18, 50, 0, DateTimeKind.Utc) },
            new Product { Id = 14, Name = "Bicicleta Estática", Price = 1299900m, Stock = 6, CategoryId = 3, FechaCreacion = new DateTime(2026, 2, 11, 15, 35, 0, DateTimeKind.Utc) },
            new Product { Id = 15, Name = "Raqueta de Tenis", Price = 249900m, Stock = 14, CategoryId = 3, FechaCreacion = new DateTime(2026, 2, 14, 20, 0, 0, DateTimeKind.Utc) },
            new Product { Id = 16, Name = "Cien Años de Soledad", Price = 59900m, Stock = 40, CategoryId = 4, FechaCreacion = new DateTime(2026, 2, 17, 14, 45, 0, DateTimeKind.Utc) },
            new Product { Id = 17, Name = "El Quijote", Price = 49900m, Stock = 35, CategoryId = 4, FechaCreacion = new DateTime(2026, 2, 20, 17, 0, 0, DateTimeKind.Utc) },
            new Product { Id = 18, Name = "Clean Code", Price = 129900m, Stock = 20, CategoryId = 4, FechaCreacion = new DateTime(2026, 2, 23, 19, 30, 0, DateTimeKind.Utc) },
            new Product { Id = 19, Name = "Café Molido 500 g", Price = 24900m, Stock = 100, CategoryId = 5, FechaCreacion = new DateTime(2026, 2, 26, 13, 20, 0, DateTimeKind.Utc) },
            new Product { Id = 20, Name = "Chocolate Negro 70%", Price = 12900m, Stock = 120, CategoryId = 5, FechaCreacion = new DateTime(2026, 3, 1, 21, 10, 0, DateTimeKind.Utc) });
    }
}
