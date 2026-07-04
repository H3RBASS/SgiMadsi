using SgiMadsi.Shared.Domain;
using SgiMadsi.Shared.Services;

namespace SgiMadsi.Dashboard.Services
{
    public class DashService
    {

    //     // Método para obtener todos los productos
    //     public List<Producto> GetAllProductos()
    //     {
    //         ListaProductos.Count();
    //         return ListaProductos;
    //     }
        
    //     public List<Producto> GetOutStockProductos()
    //     {
    //         return ListaProductos.Where(p => p.Cantidad == 0).ToList();
    //     }

    //     //metodo para contar todos los productos
    //     public int CountAllProductos()
    //     {
    //         return ListaProductos.Count;
    //     }
    //     //Metodo para agregar un nuevo producto
    //     public void AddProducto(Producto nuevoProducto)
    //     {
    //         ListaProductos.Add(nuevoProducto);
    //     }
    //     public void CrearProducto(string nombre, string categoria, decimal precio, 
    //     int cantidad)
    //     {
    //          // Creamos un objeto Producto (producto real)
    //          Producto NuevoProducto = new Producto
    //         {
    //             Nombre = nombre,
    //             Categoria = categoria,
    //             Precio = precio,
    //             Cantidad = cantidad,
    //         };
    //         // Llamamos al servicio para guardar el producto en la lista
    //         AddProducto(NuevoProducto);
    //     }

    //     //Metodo para obtener el precio total de todos los productoos
    //     public decimal GetTotalPrice()
    //     {
    //         decimal total = 0;
    //         foreach (var producto in ListaProductos)
    //         {
    //             total += producto.Precio ?? 0;
    //         }
    //         return total;
    //     }
    //     public decimal GetAveragePrice()
    //     {
    //         if (ListaProductos.Count == 0)
    //             return 0;

    //         int total = Convert.ToInt32(GetTotalPrice());
    //         return total / ListaProductos.Count;

    //     }
    //     public int GetOutStockCount()
    //     {
    //         int outOfStock = 0;
    //         foreach (var producto in ListaProductos)
    //         {
    //             if (producto.Cantidad == 0)
    //             {
    //                 outOfStock++;
    //             }
    //         }
    //         return outOfStock;
    //     }

    //     public int GetLowStockCount()
    //     {
    //         int lowStock = 0;
    //         foreach (var producto in ListaProductos)
    //         {
    //             if (producto.Cantidad < 10)
    //             {
    //                 lowStock++;
    //             }
    //         }
    //         return lowStock;
    //     }
    }
}