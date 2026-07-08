using SgiMadsi.Shared.Domain;
using SgiMadsi.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SgiMadsi.Caja.Services
{
    public class VentaService
    {
        // private readonly ProductoService _productoService;
        // private List<CarritoItem> _carrito = new();

        // public VentaService(ProductoService productoService)
        // {
        //     _productoService = productoService;
        // }

        // // === PROPIEDADES PÚBLICAS ===
        // public List<CarritoItem> Carrito => _carrito.ToList();
        // public decimal TotalCarrito => _carrito.Sum(item => item.Subtotal);
        // public int CantidadProductos => _carrito.Sum(item => item.CantidadSeleccionada);

        // // === MÉTODOS PARA EL CARRITO ===

        // /// <summary>
        // /// Agrega un producto al carrito
        // /// </summary>
        // /// <returns>True si se agregó correctamente, False si hubo error</returns>
        // public bool AgregarAlCarrito(int productoId, int cantidad)
        // {
        //     if (cantidad <= 0)
        //         return false;

        //     var producto = _productoService.GetById(productoId);
        //     if (producto == null)
        //         return false;

        //     if (cantidad > producto.Cantidad)
        //         return false;

        //     var existente = _carrito.FirstOrDefault(item => item.Id == productoId);

        //     if (existente != null)
        //     {
        //         // Validar que no exceda el stock total
        //         if (existente.CantidadSeleccionada + cantidad > existente.CantidadDisponible)
        //             return false;

        //         existente.CantidadSeleccionada += cantidad;
        //     }
        //     else
        //     {
        //         _carrito.Add(new CarritoItem
        //         {
        //             Id = producto.Id,
        //             Nombre = producto.Nombre,
        //             Categoria = producto.Categoria,
        //             Precio = producto.Precio!.Value,
        //             CantidadDisponible = producto.Cantidad,
        //             CantidadSeleccionada = cantidad
        //         });
        //     }

        //     return true;
        // }

        // /// <summary>
        // /// Elimina un producto del carrito
        // /// </summary>
        // public void EliminarDelCarrito(int productoId)
        // {
        //     var item = _carrito.FirstOrDefault(p => p.Id == productoId);
        //     if (item != null)
        //         _carrito.Remove(item);
        // }

        // /// <summary>
        // /// Actualiza la cantidad de un producto en el carrito
        // /// </summary>
        // public bool ActualizarCantidad(int productoId, int nuevaCantidad)
        // {
        //     var item = _carrito.FirstOrDefault(p => p.Id == productoId);
        //     if (item == null)
        //         return false;

        //     if (nuevaCantidad <= 0)
        //     {
        //         _carrito.Remove(item);
        //         return true;
        //     }

        //     if (nuevaCantidad > item.CantidadDisponible)
        //         return false;

        //     item.CantidadSeleccionada = nuevaCantidad;
        //     return true;
        // }

        // /// <summary>
        // /// Vacía todo el carrito
        // /// </summary>
        // public void VaciarCarrito()
        // {
        //     _carrito.Clear();
        // }

        // // === MÉTODOS PARA FINALIZAR LA VENTA ===

        // /// <summary>
        // /// Procesa la venta y descuenta el stock
        // /// </summary>
        // /// <returns>True si la venta fue exitosa, False si hubo error</returns>
        // public bool FinalizarVenta(string metodoPago)
        // {
        //     if (!_carrito.Any())
        //         return false;

        //     if (string.IsNullOrWhiteSpace(metodoPago))
        //         return false;

        //     // Validar que haya stock suficiente para todos los productos
        //     foreach (var item in _carrito)
        //     {
        //         var producto = _productoService.GetById(item.Id);
        //         if (producto == null || item.CantidadSeleccionada > producto.Cantidad)
        //             return false;
        //     }

        //     // Descontar stock de cada producto
        //     foreach (var item in _carrito)
        //     {
        //         var producto = _productoService.GetById(item.Id);
        //         if (producto != null)
        //         {
        //             producto.Cantidad -= item.CantidadSeleccionada;
        //             // Aquí podrías llamar a un método Update en ProductoService
        //             // _productoService.Update(producto);
        //         }
        //     }

        //     // Aquí guardarías la venta en la base de datos
        //     // Ejemplo: _ventaRepository.GuardarVenta(new Venta { ... });

        //     // Limpiar carrito
        //     _carrito.Clear();

        //     return true;
        // }

        // // === MÉTODOS PARA BÚSQUEDA ===

        // /// <summary>
        // /// Busca productos por nombre o categoría
        // /// </summary>
        // public List<Producto> BuscarProductos(string searchTerm)
        // {
        //     if (string.IsNullOrWhiteSpace(searchTerm))
        //         return _productoService.GetAllProductos();

        //     return _productoService.GetAllProductos()
        //         .Where(p => p.Nombre.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
        //                     p.Categoria.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
        //         .ToList();
        // }

        // /// <summary>
        // /// Obtiene un producto por ID
        // /// </summary>
        // public Producto? GetProductoById(int id)
        // {
        //     return _productoService.GetById(id);
        // }
    }
}