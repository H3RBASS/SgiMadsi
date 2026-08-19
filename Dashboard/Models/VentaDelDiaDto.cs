namespace SgiMadsi.Dashboard.Models;

public class VentaDelDiaDto
{
    public int VentaId { get; set; }
    public DateTime Fecha { get; set; }
    public decimal Total { get; set; }
    public string MetodoPago { get; set; } = string.Empty;
    public decimal Descuento { get; set; }
    public int CantidadItems { get; set; }
    public List<ProductoVentaDto> Productos { get; set; } = new();
}

public class ProductoVentaDto
{
    public string Nombre { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }
}
