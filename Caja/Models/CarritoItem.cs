namespace SgiMadsi.Caja.Models;

public class CarritoItem
{
    public int ProductoId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal PrecioVenta { get; set; }
    public int Cantidad { get; set; } = 1;
    public string Subcategoria {get; set; } = string.Empty;
}