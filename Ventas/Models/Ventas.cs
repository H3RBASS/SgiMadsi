using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

public class VentasTabla:BaseModel
{
    [PrimaryKey("id_venta", false)]
    public int Id { get; set; }

    [Column("producto")]
    public string Producto { get; set; } = string.Empty;
    
    [Column("fecha")]
    public DateTime Fecha { get; set; }
    
    [Column("precio")]
    public decimal PrecioProducto { get; set; }
    
    [Column("metodopago")]
    public string MetodoPago { get; set; } = string.Empty;
}
