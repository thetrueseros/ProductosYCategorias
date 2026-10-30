using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductosYCategorias.Models;

/// <summary>
/// Producto perteneciente a una <see cref="Category"/>.
/// </summary>
public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los {1} caracteres.")]
    [Display(Name = "Nombre")]
    public string Name { get; set; } = string.Empty;

    [Range(typeof(decimal), "0.01", "9999999999999999.99",
        ParseLimitsInInvariantCulture = true,
        ConvertValueInInvariantCulture = true,
        ErrorMessage = "El precio debe ser mayor que cero.")]
    [Column(TypeName = "decimal(18,2)")]
    [DataType(DataType.Currency)]
    [Display(Name = "Precio (COP)")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
    [Display(Name = "Stock")]
    public int Stock { get; set; }

    [Display(Name = "Categoría")]
    public int CategoryId { get; set; }

    [Display(Name = "Categoría")]
    public Category? Category { get; set; }

    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = false)]
    [Display(Name = "Fecha de creación")]
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
}
