namespace ProductosYCategorias.Models;

/// <summary>
/// DTO de solo cálculo para la paginación. No contiene lógica de rutas:
/// la vista parcial <c>_Pagination</c> reconstruye los enlaces a partir del query string.
/// </summary>
public class PageInfo
{
    public int TotalItems { get; set; }

    public int ItemsPerPage { get; set; }

    public int CurrentPage { get; set; }

    public int TotalPages => ItemsPerPage <= 0
        ? 0
        : (int)Math.Ceiling(TotalItems / (double)ItemsPerPage);

    public bool HasPrevious => CurrentPage > 1;

    public bool HasNext => CurrentPage < TotalPages;

    /// <summary>Número del primer elemento de la página actual (0 si no hay datos).</summary>
    public int FirstItem => TotalItems == 0
        ? 0
        : ((CurrentPage - 1) * ItemsPerPage) + 1;

    /// <summary>Número del último elemento de la página actual.</summary>
    public int LastItem => Math.Min(CurrentPage * ItemsPerPage, TotalItems);
}
