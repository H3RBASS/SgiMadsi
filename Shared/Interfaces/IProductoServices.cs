using SgiMadsi.Shared.Domain;

namespace SgiMadsi.Shared.Interfaces;

public interface IProductoServices
{
    Task<List<Producto>> ObtenerProductosAsync();
    Task<Producto?> ObtenerProductoPorIdAsync(int id);
    Task CrearProductoAsync(Producto producto);
    Task EditarProductoAsync(Producto producto);
    Task EliminarProductoAsync(int id);
    Task<int> ObtenerProductoSPC();

}
