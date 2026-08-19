using SgiMadsi.Shared.Interfaces;

namespace SgiMadsi.Dashboard.Services;

public interface IDashService
{
    Task<List<Models.VentaDelDiaDto>> ObtenerVentasDelDiaAsync();
}

public class DashService : IDashService
{
    private readonly IVentaServices _ventaServices;
    private readonly IDetalleVentaServices _detalleVentaServices;
    private readonly IProductoServices _productoServices;

    private const int OffsetBoliviaHoras = -4;

    public DashService(
        IVentaServices ventaServices,
        IDetalleVentaServices detalleVentaServices,
        IProductoServices productoServices)
    {
        _ventaServices = ventaServices;
        _detalleVentaServices = detalleVentaServices;
        _productoServices = productoServices;
    }

    public async Task<List<Models.VentaDelDiaDto>> ObtenerVentasDelDiaAsync()
    {
        var ahoraBolivia = DateTime.UtcNow.AddHours(OffsetBoliviaHoras);
        var inicioDiaBolivia = new DateTime(ahoraBolivia.Year, ahoraBolivia.Month, ahoraBolivia.Day);
        var inicioDia = DateTime.SpecifyKind(inicioDiaBolivia.AddHours(-OffsetBoliviaHoras), DateTimeKind.Utc);
        var finDia = DateTime.SpecifyKind(inicioDia.AddDays(1).AddTicks(-1), DateTimeKind.Utc);

        var ventas = await _ventaServices.ObtenerVentasPorFechaAsync(inicioDia, finDia);

        var productos = await _productoServices.ObtenerProductosAsync();
        var mapaProductos = productos.ToDictionary(p => p.Id, p => p.Nombre);

        var dtos = new List<Models.VentaDelDiaDto>();

        foreach (var venta in ventas)
        {
            var detalles = await _detalleVentaServices.ObtenerDetallesVentaAsync(venta.Id);

            var productosVenta = detalles
                .Select(d => new Models.ProductoVentaDto
                {
                    Nombre = mapaProductos.TryGetValue(d.ProductoId, out var nombre) ? nombre : $"#{d.ProductoId}",
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    Subtotal = d.Subtotal
                })
                .ToList();

            dtos.Add(new Models.VentaDelDiaDto
            {
                VentaId = venta.Id,
                Fecha = venta.Fecha,
                Total = venta.Total,
                MetodoPago = venta.MetodoPago,
                Descuento = venta.Descuento,
                CantidadItems = detalles.Sum(d => d.Cantidad),
                Productos = productosVenta
            });
        }

        return dtos.OrderByDescending(d => d.Fecha).ToList();
    }
}
