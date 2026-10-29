using System.ComponentModel.DataAnnotations;

namespace ProductosYCategorias.Models;

/// <summary>
/// Categoría que agrupa productos. Relación 1:N con <see cref="Product"/>.
/// </summary>
public class Category
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los {1} caracteres.")]
    [Display(Name = "Nombre")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "La descripción no puede superar los {1} caracteres.")]
    [Display(Name = "Descripción")]
    public string? Description { get; set; }

    [Display(Name = "Productos")]
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
