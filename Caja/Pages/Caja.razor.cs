using Microsoft.AspNetCore.Components;
using SgiMadsi.Caja.Services;
using SgiMadsi.Shared.Domain;
using System.Collections.Generic;
using System.Linq;

namespace SgiMadsi.Dashboard.Pages
{
    public partial class Ventas
    {
        [Inject]
        private VentaService ventaService { get; set; } = default!;

        // === PROPIEDADES LOCALES (SOLO PARA LA UI) ===
        private string searchTerm = string.Empty;
        private Producto? productoSeleccionado;
        private int cantidadSeleccionada = 1;
        private string metodoPago = string.Empty;

        // === PROPIEDADES QUE OBTIENEN DATOS DEL SERVICIO ===
        private List<Producto> ProductosFiltrados => ventaService.BuscarProductos(searchTerm);
        private List<CarritoItem> Carrito => ventaService.Carrito;
        private decimal TotalCarrito => ventaService.TotalCarrito;
        private int CantidadProductos => ventaService.CantidadProductos;

        // === MÉTODOS DE UI ===

        protected void SeleccionarProducto(Producto producto)
        {
            productoSeleccionado = producto;
            cantidadSeleccionada = 1;
            searchTerm = string.Empty;
        }

        protected void AgregarAlCarrito()
        {
            if (productoSeleccionado == null)
                return;

            var exito = ventaService.AgregarAlCarrito(productoSeleccionado.Id, cantidadSeleccionada);
            
            if (exito)
            {
                productoSeleccionado = null;
                cantidadSeleccionada = 1;
            }
            // else: mostrar mensaje de error (stock insuficiente)
        }

        protected void EliminarDelCarrito(CarritoItem item)
        {
            ventaService.EliminarDelCarrito(item.Id);
        }

        protected void ActualizarCantidadCarrito(CarritoItem item, int nuevaCantidad)
        {
            ventaService.ActualizarCantidad(item.Id, nuevaCantidad);
        }

        protected void FinalizarVenta()
        {
            if (string.IsNullOrWhiteSpace(metodoPago))
                return;

            var exito = ventaService.FinalizarVenta(metodoPago);
            
            if (exito)
            {
                metodoPago = string.Empty;
                // Mostrar mensaje de éxito
            }
            // else: mostrar mensaje de error
        }

        protected void VaciarCarrito()
        {
            ventaService.VaciarCarrito();
        }
    }
}