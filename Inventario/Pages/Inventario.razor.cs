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
    
    private List<Producto> _productos = new();
    
    protected override async Task OnInitializedAsync()
    {
        _productos = await ProductoServices.ObtenerProductosAsync();
    }
    private void OrdenarProductos()
    {
        if (_productoFiltro)
        {
            _productos = _productos.OrderBy(p => p.Nombre).ToList();
            _productoFiltro = false;
        }
        else
        {
            _productos = _productos.OrderByDescending(p => p.Nombre).ToList();
            _productoFiltro = true;
        }
    }

    private void OrdenarCategorias()
    {
        if (_categoriaFiltro)
        {
            _productos = _productos.OrderBy(p => p.Categoria).ToList();
            _categoriaFiltro = false;
        }
        else
        {
            _productos = _productos.OrderByDescending(p => p.Categoria).ToList();
            _categoriaFiltro = true;
        }
    }

    private void OrdenarPrecios()
    {
        if (_precioFiltro)
        {
            _productos = _productos.OrderBy(p => p.PrecioVenta).ToList();
            _precioFiltro = false;
        }
        else
        {
            _productos = _productos.OrderByDescending(p => p.PrecioVenta).ToList();
            _precioFiltro = true;
        }
    }
    
    private void OrdenarCantidad()
    {
        if (_precioFiltro)
        {
            _productos = _productos.OrderBy(p => p.Stock).ToList();
            _precioFiltro = false;
        }
        else
        {
            _productos = _productos.OrderByDescending(p => p.Stock).ToList();
            _precioFiltro = true;
        }
    }
}

