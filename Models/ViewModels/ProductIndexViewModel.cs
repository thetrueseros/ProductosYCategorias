namespace ProductosYCategorias.Models.ViewModels;

/// <summary>
/// Modelo de la vista de listado de productos: datos, filtros aplicados y paginación.
/// </summary>
public class ProductIndexViewModel
{
    public IReadOnlyList<Product> Products { get; init; } = Array.Empty<Product>();

    public IReadOnlyList<Category> Categories { get; init; } = Array.Empty<Category>();

    public PageInfo PageInfo { get; init; } = new();

    public string? Search { get; init; }

    public int? CategoryId { get; init; }

    public string? SortOrder { get; init; }

    public int PageSize { get; init; } = 8;

    public bool HasFilters => !string.IsNullOrWhiteSpace(Search) || CategoryId.HasValue;

    public bool IsEmpty => Products.Count == 0;
}
