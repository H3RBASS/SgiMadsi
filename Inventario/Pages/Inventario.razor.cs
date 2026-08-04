using Microsoft.AspNetCore.Components;
using SgiMadsi.Shared.Domain;
using SgiMadsi.Shared.Interfaces;
using SgiMadsi.Shared.Services;

namespace SgiMadsi.Inventario.Pages;
public partial class Inventario
{
    [Inject]
    private IProductoServices ProductoServices { get; set; } = default!;
    private bool _mostrarModalEditar { get; set; }
    private bool _productoFiltro { get; set; }
    private bool _categoriaFiltro { get; set; }
    private bool _precioFiltro { get; set; }
    private string _busqueda = string.Empty;
    private Producto _productoEditando = new Producto();
    private bool _procesando;
    private bool _mensajeConfirmacion;


    //lista de productos inventario puede cambiar cuando se hace la busqueda
    private List<Producto> _productosFiltrados = new();
    
    //lista de productos obtenidos del servicio
    private List<Producto> _productosOriginales = new();


    //metodos o funciones    
    protected override async Task OnInitializedAsync()
    {
        await ObtenerProductos();
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
    
    private async Task ObtenerProductos()
    {
        _productosOriginales = await ProductoServices.ObtenerProductosAsync();
        _productosOriginales = _productosOriginales.OrderBy(p => p.Subcategoria).ToList();
        _productosFiltrados = _productosOriginales.ToList();
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

    //metodos para el modal EDITAR
    private async Task ObtenerProductoEditar(Producto producto)
    {
        _productoEditando.Id = producto.Id;
        _productoEditando.Nombre = producto.Nombre;
        _productoEditando.Categoria = producto.Categoria;
        _productoEditando.Presentacion = producto.Presentacion;
        _productoEditando.Unidad = producto.Unidad;
        _productoEditando.Subcategoria = producto.Subcategoria;
        _productoEditando.PrecioVenta = producto.PrecioVenta;
        _productoEditando.PrecioCompra = producto.PrecioCompra;
        _productoEditando.Stock = producto.Stock;
        _mostrarModalEditar = true;
    }
private async Task GuardarCambios()
{
    if (_procesando)
        return;

    _procesando = true;

    try
    {
        _productoEditando.Nombre = LimpiarTexto(_productoEditando.Nombre);
        _productoEditando.Subcategoria = LimpiarTexto(_productoEditando.Subcategoria);

        await ProductoServices.EditarProductoAsync(_productoEditando);

        var producto = _productosOriginales.First(p => p.Id == _productoEditando.Id);
        producto.CopiarDesde(_productoEditando);

    }

    catch (Exception ex)
    {
        Console.WriteLine($"Error al guardar cambios: {ex.Message}");
    }

    finally
    {
        _mensajeConfirmacion = true;
        _procesando = false;
        StateHasChanged();

        await Task.Delay(2000);

        _mensajeConfirmacion = false;
        StateHasChanged();
    }
}

    private void CerrarModal()
    {
        _mostrarModalEditar = false;
        _productoEditando = new Producto();
    }

    private static string LimpiarTexto(string texto)
    {
        // La cadena de ej es "oLa maximus  Antigrasa"
        // Limpiar espacios multiples
        var resultado = texto.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        //en resultado tenemos el arreglo con las palabras ["oLa ", "maximus ","Antigrasa"]
        for (int i = 0 ; i < resultado.Length; i++)
        {
            //obtenemos la primer palabra del arreglo
            string palabraActual = resultado[i];
            //obtenemos la primer letra
            char primerLetra = char.ToUpper(palabraActual[0]);
            //obtenemos el resto de la palabra desde X posicion en adelante y en minusculas
            string restoPalabra = palabraActual.Substring(1).ToLower();
            //creamos la cadena contatenando las partes
            resultado[i] = $"{primerLetra}{restoPalabra}";
        }
        //Retornamos la palabra uniendo
        return string.Join(" ", resultado);
    } 

}