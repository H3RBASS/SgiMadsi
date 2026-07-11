using Microsoft.AspNetCore.Components;
using SgiMadsi.Shared.Domain;
using SgiMadsi.Shared.Interfaces;
using SgiMadsi.Shared.Services;

namespace SgiMadsi.Inventario.Pages;
public partial class Inventario
{
    [Inject]
    private IProductoServices ProductoServices { get; set; } = default!;
    private bool _productoFiltro { get; set; }
    private bool _categoriaFiltro { get; set; }
    private bool _precioFiltro { get; set; }
    private string _busqueda = string.Empty;


    //lista de productos inventario puede cambiar cuando se hace la busqueda
    private List<Producto> _productosFiltrados = new();
    
    //lista de productos obtenidos del servicio
    private List<Producto> _productosOriginales = new();


    //metodos o funciones    
    protected override async Task OnInitializedAsync()
    {
        _productosOriginales = await ProductoServices.ObtenerProductosAsync();
        _productosOriginales = _productosOriginales.OrderBy(p => p.Subcategoria).ToList();
        _productosFiltrados = _productosOriginales.ToList();
    }
private void BuscarProductos()
{
    // Eliminamos los espacios en blanco al inicio y al final de la búsqueda
    var busqueda = _busqueda.Trim();

    if (string.IsNullOrWhiteSpace(busqueda))
    {
        _productosFiltrados = _productosOriginales.ToList();
        return;
    }
    //Separamos la cadena en palabras y buscamos cada palabra en el nombre y la subcategoria del producto
    var palabrasBusqueda = busqueda.Split(' ',
        StringSplitOptions.RemoveEmptyEntries);

        _productosFiltrados = _productosOriginales
        .Where(p =>
        {
            var textoBusqueda =
                 $"{p.Subcategoria} {p.Nombre}";
                 
            return palabrasBusqueda.Any(palabra =>
                 textoBusqueda.Contains(
                 palabra,
                 StringComparison.OrdinalIgnoreCase));
        })
        .ToList();
}
    private void OrdenarProductos()
    {
        if (_productoFiltro)
        {
            _productosFiltrados = _productosFiltrados.OrderBy(p => p.Subcategoria).ToList();
            _productoFiltro = false;
        }
        else
        {
            _productosFiltrados = _productosFiltrados.OrderByDescending(p => p.Subcategoria).ToList();
            _productoFiltro = true;
        }
    }

    private void OrdenarCategorias()
    {
        if (_categoriaFiltro)
        {
            _productosFiltrados = _productosFiltrados.OrderBy(p => p.Categoria).ToList();
            _categoriaFiltro = false;
        }
        else
        {
            _productosFiltrados = _productosFiltrados.OrderByDescending(p => p.Categoria).ToList();
            _categoriaFiltro = true;
        }
    }

    private void OrdenarPrecios()
    {
        if (_precioFiltro)
        {
            _productosFiltrados = _productosFiltrados.OrderBy(p => p.PrecioVenta).ToList();
            _precioFiltro = false;
        }
        else
        {
            _productosFiltrados = _productosFiltrados.OrderByDescending(p => p.PrecioVenta).ToList();
            _precioFiltro = true;
        }
    }
    
    private void OrdenarCantidad()
    {
        if (_precioFiltro)
        {
            _productosFiltrados = _productosFiltrados.OrderBy(p => p.Stock).ToList();
            _precioFiltro = false;
        }
        else
        {
            _productosFiltrados = _productosFiltrados.OrderByDescending(p => p.Stock).ToList();
            _precioFiltro = true;
        }
    }
}

