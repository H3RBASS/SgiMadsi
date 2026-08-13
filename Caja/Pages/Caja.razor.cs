using Microsoft.AspNetCore.Components;
using SgiMadsi.Caja.Models;
using SgiMadsi.Shared.Domain;
using SgiMadsi.Shared.Interfaces;

namespace SgiMadsi.Caja.Pages
{
    public partial class Caja
    {
        [Inject]
        private IProductoServices ProductoServices { get; set; } = default!;
        
        [Inject]
        private IVentaServices VentaServices { get; set; } = default!;

        [Inject]
        private IDetalleVentaServices DetalleVentaServices { get; set; } = default!;
        
        private List<Producto> _productosOriginales = new();
        private List<Producto> _productosFiltrados = new();
        private List<Producto> _productosSeleccionados = new();
        private List<CarritoItem> _carrito = new();
        private List<string> Categorias { get; set; } = new();

        private Checkout _checkout = new();

        private string _busqueda = string.Empty;

        private bool MostrarModalVenta;
        
        private bool _procesando;

        protected override async Task OnInitializedAsync()
        {
            _productosOriginales = await ProductoServices.ObtenerProductosAsync();
            _productosOriginales = _productosOriginales.OrderBy(p => p.Subcategoria).ToList();
            _productosFiltrados = _productosOriginales.ToList();

            Categorias = _productosOriginales.Select(p => p.Subcategoria)
            .Distinct()
            .ToList(); 
        }
        private void AgregarAlCarrito(Producto productoOriginal)
        {
            var producto = _carrito.FirstOrDefault(p => p.Nombre == productoOriginal.Nombre);
            if (producto != null)
            {
                producto.Cantidad++;
            }
            else
            {
               _carrito.Add(new CarritoItem
               {
                    ProductoId = productoOriginal.Id,
                    Nombre = productoOriginal.Nombre,
                    Cantidad = 1,
                    PrecioVenta = productoOriginal.PrecioVenta,
                    Subcategoria = productoOriginal.Subcategoria
               });
            }
            
        }

        private void CarritoEstado()
        {
            MostrarModalVenta = _carrito?.Any() == true;

            
        }
        private void LimpiarCarrito()   
        {
            _carrito.Clear();
        }
        private async Task RegistrarVenta()
        {
            if  (_procesando)
            
                return;
                _procesando = true ;
            
            if(_carrito == null || _carrito.Count == 0)
            {
                return;
            }
            var venta = new Venta

            {
                Fecha = DateTime.UtcNow,
                Total = _carrito.Sum(x => x.Cantidad * x.PrecioVenta),
                MetodoPago = _checkout.MetodoPago,
                Descuento = _checkout.Descuento

            };
            var ventaCreada = await VentaServices.CrearVentaAsync(venta);

            if(ventaCreada != null)
            {
                foreach(var item in _carrito)
                {
                    var detalle = new DetalleVenta
                        {
                            VentaId = ventaCreada.Id,
                            ProductoId = item.ProductoId,
                            Cantidad = item.Cantidad,
                            PrecioUnitario = item.PrecioVenta,
                            Subtotal = item.Cantidad * item.PrecioVenta
                        };

                    await DetalleVentaServices.CrearDetalleVentaAsync(detalle);
                }

                foreach(var item in _carrito)
                {
                    var producto = await ProductoServices.ObtenerProductoPorIdAsync(item.ProductoId);
                    if(producto != null)
                    {
                        // producto.Stock -= item.Cantidad;
                        await ProductoServices.EditarProductoAsync(producto);
                    }
                }
                _procesando = false;
            }
            LimpiarCarrito();
            CarritoEstado();
        }

        private void SumarCantidad(CarritoItem item)
        {
            item.Cantidad++;
        }
        
        private void RestarCantidad(CarritoItem item)
        {
            if (item.Cantidad > 1)
            {
                item.Cantidad--;
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

        private void EliminarProducto(CarritoItem productoOriginal)
        {
            _carrito.RemoveAll(p => p.Nombre == productoOriginal.Nombre);
        }
        
        private void LimpiarBusqueda()
        {
            _busqueda = string.Empty;
            _productosFiltrados = _productosOriginales.ToList();
        }

    }
}
