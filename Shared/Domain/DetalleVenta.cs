using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace SgiMadsi.Shared.Domain;

[Table("DetalleVentas")]
public class DetalleVenta : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }

    [Column("ventaId")]
    public int VentaId { get; set; }

    [Column("productoId")]
    public int ProductoId { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; }
    
    [Column("precioUnitario")]
    public decimal PrecioUnitario { get; set; }

    [Column("subtotal")]
    public decimal Subtotal { get; set; }
}