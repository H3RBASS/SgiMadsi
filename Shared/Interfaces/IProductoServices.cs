using SgiMadsi.Shared.Domain;

namespace SgiMadsi.Shared.Interfaces;

public interface IProductoServices
{
    Task<List<Producto>> ObtenerProductosAsync();
    Task CrearProductoAsync(Producto producto);
}
