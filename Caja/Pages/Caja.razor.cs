using System.Globalization;
using System.Text;
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
        private List<string[]> _productosTokens = new();
        private List<CarritoItem> _carrito = new();
        private List<string> Categorias { get; set; } = new();
        private List<Venta> Ventas = new();

        private Checkout _checkout = new();

        private string _busqueda = string.Empty;

        private bool MostrarModalVenta;
        private bool MostrarModalArqueo;
        
        private bool _procesando;

        protected override async Task OnInitializedAsync()
        {
            _productosOriginales = await ProductoServices.ObtenerProductosAsync();
            _productosOriginales = _productosOriginales.OrderBy(p => p.Subcategoria).ToList();
            _productosFiltrados = _productosOriginales.ToList();
            _productosTokens = _productosOriginales
                .Select(p => Tokenizar(NormalizarTexto($"{p.Subcategoria} {p.Nombre}")))
                .ToList();
            Ventas = await VentaServices.ObtenerVentasAsync();
            Categorias = _productosOriginales.Select(p => p.Subcategoria)
            .Distinct()
            .ToList(); 
        }
        private List<Venta> VentasDelDia =>
            Ventas.Where(v => v.Fecha.Date == DateTime.UtcNow).ToList();
        private decimal TotalVentas =>
            VentasDelDia.Sum(x => x.Total);

        private decimal TotalQR =>
            VentasDelDia
                .Where(x => x.MetodoPago == "QR")
                .Sum(x => x.Total);

        private decimal TotalEfectivo =>
            VentasDelDia
                .Where(x => x.MetodoPago == "Efectivo")
                .Sum(x => x.Total);
        private void AgregarAlCarrito(Producto productoOriginal)
        {
            // [FIX #9] Comparar por ProductoId en vez de Nombre para evitar mezclar productos distintos con mismo nombre
            var producto = _carrito.FirstOrDefault(p => p.ProductoId == productoOriginal.Id);
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
            // [FIX #2] Sin {} solo la primera línea era el cuerpo del if, _procesando siempre se ejecutaba
            if (_procesando)
                return;

            if (_carrito == null || _carrito.Count == 0)
            {
                return;
            }

            _procesando = true;

            var ahoraUtc = DateTime.UtcNow;
            var fechaVenta = _checkout.FechaVenta.HasValue
                ? DateTime.SpecifyKind(_checkout.FechaVenta.Value.Date, DateTimeKind.Utc).Add(ahoraUtc.TimeOfDay)
                : ahoraUtc;

            var venta = new Venta

            {
                Fecha = fechaVenta,
                Total = _carrito.Sum(x => x.Cantidad * x.PrecioVenta),
                MetodoPago = _checkout.MetodoPago,
                Descuento = _checkout.Descuento
                
            };
            try
            {
                var ventaCreada = await VentaServices.CrearVentaAsync(venta);

                if (ventaCreada != null)
                {
                    foreach (var item in _carrito)
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

                    foreach (var item in _carrito)
                    {
                        var producto = await ProductoServices.ObtenerProductoPorIdAsync(item.ProductoId);
                        if (producto != null)
                        {
                            // producto.Stock -= item.Cantidad;
                            await ProductoServices.EditarProductoAsync(producto);
                        }
                    }

                    // [FIX #7] Solo limpiar carrito y cerrar modal si la venta fue exitosa
                    LimpiarCarrito();
                    MostrarModalVenta = false;
                    _checkout.Descuento = 0;
                    _checkout.FechaVenta = null;
                }

                Ventas = await VentaServices.ObtenerVentasAsync();
            }
            finally
            {
                // [FIX #5] Siempre resetear _procesando, incluso si hay excepción
                _procesando = false;
            }
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

            // Si la consulta termina en espacio, la última palabra se trata como palabra completa
            var terminaEnEspacio = _busqueda.Length > 0 && char.IsWhiteSpace(_busqueda[_busqueda.Length - 1]);

            var palabrasBusqueda = Tokenizar(NormalizarTexto(busqueda));
            if (palabrasBusqueda.Length == 0)
            {
                _productosFiltrados = _productosOriginales.ToList();
                return;
            }

            // Todas las palabras salvo la última deben coincidir como palabra completa (AND)
            var ultimaPalabra = palabrasBusqueda[^1];
            var palabrasCompletas = terminaEnEspacio
                ? palabrasBusqueda
                : palabrasBusqueda.Take(palabrasBusqueda.Length - 1).ToArray();

            _productosFiltrados = _productosOriginales
                .Where((p, i) =>
                {
                    var tokens = _productosTokens[i];

                    if (!palabrasCompletas.All(w => tokens.Contains(w)))
                    {
                        return false;
                    }

                    // Sin espacio final, la última palabra puede ser prefijo de una palabra del producto
                    return terminaEnEspacio || tokens.Any(t => t.StartsWith(ultimaPalabra));
                })
                .ToList();
        }

        private static string NormalizarTexto(string texto)
        {
            var formD = texto.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder(formD.Length);
            foreach (var c in formD)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }
            return sb.ToString().Normalize(NormalizationForm.FormC).ToLowerInvariant();
        }

        private static string[] Tokenizar(string texto)
        {
            var sinPuntuacion = new string(texto.Select(c => char.IsLetterOrDigit(c) ? c : ' ').ToArray());
            return sinPuntuacion.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }

        private void EliminarProducto(CarritoItem productoOriginal)
        {
            _carrito.RemoveAll(p => p.Nombre == productoOriginal.Nombre);
        }
        
        private void CancelarVenta()
        {
            MostrarModalVenta = false;
        }
        
        private void LimpiarBusqueda()
        {
            _busqueda = string.Empty;
            _productosFiltrados = _productosOriginales.ToList();
        }

    }
}
