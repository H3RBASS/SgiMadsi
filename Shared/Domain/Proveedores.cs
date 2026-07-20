using System.ComponentModel.DataAnnotations;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace SgiMadsi.Shared.Domain;

[Table("Proveedores")]
public class Proveedores : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }
    
    [Column("created_at")]
    public DateTime CreateAt { get; set; }
    
    [Column("proveedor")]
    [Required(ErrorMessage = "El proveedor es obligatorio")]
    public string Proveedor { get; set; } = string.Empty;

    [Column("detalle")]
    public string Detalle { get; set; } = string.Empty;

    // [Column("imagen")]
    // public string Imagen { get; set; } = string.Empty;
}