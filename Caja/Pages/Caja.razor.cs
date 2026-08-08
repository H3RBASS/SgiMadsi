using Microsoft.AspNetCore.Components;
using SgiMadsi.Shared.Domain;
using SgiMadsi.Shared.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SgiMadsi.Caja.Pages
{
    public partial class Caja
    {
        [Inject]
        private IProductoServices ProductoServices { get; set; } = default!;
        private List<Producto> _productosOriginales = new();
        private List<Producto> _productosFiltrados = new();
        private List<Producto> _productosSeleccionados = new();
        private List<string> Categorias { get; set; } = new();

        private string _busqueda = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            _productosOriginales = await ProductoServices.ObtenerProductosAsync();
            _productosOriginales = _productosOriginales.OrderBy(p => p.Subcategoria).ToList();
            _productosFiltrados = _productosOriginales.ToList();

            Categorias = _productosOriginales.Select(p => p.Subcategoria)
            .Distinct()
            .ToList(); 
        }
        private void obtenerProductoVenta(string _producto)
        {
            var producto = _productosOriginales.FirstOrDefault(p => p.Nombre == _producto);
            if (producto != null)
            {
                _productosSeleccionados.Add(producto);
            }
            
        }

        private void SumarCuenta()
        {
            if(Categorias.Any())
            {
                
            }
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

        private void LimpiarBusqueda()
        {
            _busqueda = string.Empty;
            _productosFiltrados = _productosOriginales.ToList();
        }

    }
}
