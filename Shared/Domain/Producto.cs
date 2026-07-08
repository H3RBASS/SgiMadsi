using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace SgiMadsi.Shared.Domain;

[Table("Productos")]
public class Producto : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }

    [Column("create_at")]
    public DateTime CreateAt { get; set; }

    [Column("categoria")]
    public string Categoria { get; set; } = string.Empty;

    [Column("subcategoria")]
    public string Subcategoria { get; set; } = string.Empty;

    [Column("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Column("presentacion")]
    public string Presentacion { get; set; } = string.Empty;

    [Column("unidad")]
    public string Unidad { get; set; } = string.Empty;

    [Column("stock")]
    public decimal Stock { get; set; }

    [Column("precioVenta")]
    public decimal PrecioVenta { get; set; }

    [Column("precioCompra")]
    public decimal PrecioCompra { get; set; }
}