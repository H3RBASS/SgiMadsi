using System.ComponentModel.DataAnnotations;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace SgiMadsi.Shared.Domain;

[Table("Productos")]
public class Producto : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }

    [Column("created_at")]
    public DateTime CreateAt { get; set; }

    [Column("categoria")]
    [Required(ErrorMessage = "La categoría es obligatoria")]
    public string Categoria { get; set; } = string.Empty;
    
    [Column("subcategoria")]
    public string Subcategoria { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "El nombre del producto es obligatorio")]
    [Column("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "La presentación es obligatoria")]
    [Column("presentacion")]
    public string Presentacion { get; set; } = string.Empty;

    [Required(ErrorMessage = "La unidad es obligatoria")]
    [Column("unidad")]
    public string Unidad { get; set; } = string.Empty;

    [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
    [Column("stock")]
    public int Stock { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    [Required(ErrorMessage = "El precio es obligatorio")]
    [Column("precioVenta")]
    public decimal PrecioVenta { get; set; }

    // [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    // [Required(ErrorMessage = "El precio es obligatorio")]
    [Column("precioCompra")]
    public decimal PrecioCompra { get; set; } = 0;
}
