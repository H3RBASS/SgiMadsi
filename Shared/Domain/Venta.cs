using System.ComponentModel.DataAnnotations;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace SgiMadsi.Shared.Domain;

[Table("Ventas")]
public class Venta : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }
    
    [Column("created_at")]
    public DateTime Fecha { get; set; }
    
    [Column("total")]
    public decimal Total { get; set; }
    
    [Column("tipoPago")]
    [Required(ErrorMessage = "El método de pago es requerido")]
    public string MetodoPago { get; set; } = string.Empty;

    [Column("descuento")]
    public decimal Descuento  {get; set;}

    // public List<DetalleVenta> Detalles { get; set; } = new();
}
