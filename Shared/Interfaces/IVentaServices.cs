using SgiMadsi.Shared.Domain;

namespace SgiMadsi.Shared.Interfaces;

public interface IVentaServices
{
    Task<Venta?> CrearVentaAsync(Venta venta);
    Task<List<Venta>> ObtenerVentasAsync();
    Task<List<Venta>> ObtenerVentasPorFechaAsync(DateTime desde, DateTime hasta);
}