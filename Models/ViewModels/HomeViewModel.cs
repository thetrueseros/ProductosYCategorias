namespace ProductosYCategorias.Models.ViewModels;

/// <summary>
/// Datos agregados para el dashboard de inicio. Se calculan en el controlador
/// con consultas EF para no cargar todas las entidades en memoria.
/// </summary>
public class HomeViewModel
{
    public int TotalProducts { get; init; }

    public int TotalCategories { get; init; }

    public int TotalStock { get; init; }

    public decimal InventoryValue { get; init; }

    public int LowStockCount { get; init; }

    public int OutOfStockCount { get; init; }

    public IReadOnlyList<Product> LatestProducts { get; init; } = Array.Empty<Product>();

    public IReadOnlyList<Product> LowStockProducts { get; init; } = Array.Empty<Product>();

    public IReadOnlyList<CategorySummary> TopCategories { get; init; } = Array.Empty<CategorySummary>();

    public bool IsEmpty => TotalProducts == 0;
}

/// <summary>Resumen de una categoría para las barras de progreso del dashboard.</summary>
public sealed record CategorySummary(int Id, string Name, int ProductCount, double Share);
