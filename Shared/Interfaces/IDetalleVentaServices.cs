using SgiMadsi.Shared.Domain;

namespace SgiMadsi.Shared.Interfaces;

public interface IDetalleVentaServices
{
    Task<DetalleVenta?> CrearDetalleVentaAsync(DetalleVenta detalleVenta);
    Task<List<DetalleVenta>> ObtenerDetallesVentaAsync(int ventaId);
}