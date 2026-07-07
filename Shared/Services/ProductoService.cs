using SgiMadsi.Shared.Domain;
using SgiMadsi.Shared.Interfaces;
using Supabase;

namespace SgiMadsi.Shared.Services
{
    public class ProductoService : IProductoServices
    {
        private readonly Client _client;

        public ProductoService(Client client)
        {
            _client = client;
        }

        public async Task<List<Producto>> ObtenerProductosAsync()
        {
            var response = await _client
            .From<Producto>()
            .Get();

            return response.Models;
        }

        /*
        private List<Producto> ListaProductos = new List<Producto>
        {
            new Producto { Id = 1, Nombre = "Producto A", Categoria = "Categoría 1", Precio = 10.5m, Cantidad = 5 },
            new Producto { Id = 2, Nombre = "Producto B", Categoria = "Categoría 2", Precio = 20.0m, Cantidad = 0 },
            new Producto { Id = 3, Nombre = "Producto C", Categoria = "Categoría 1", Precio = 15.75m, Cantidad = 3 },
            new Producto { Id = 4, Nombre = "Producto D", Categoria = "Categoría 3", Precio = 30.0m, Cantidad = 10 },
            new Producto { Id = 5, Nombre = "Producto E", Categoria = "Categoría 2", Precio = 25.0m, Cantidad = 0 }
        };
        public Producto? GetById(int id)
        {
            return ListaProductos.FirstOrDefault(p => p.Id == id);
        }
        public int GetOutStockCount()
        {
            return ListaProductos.Count(p => p.Cantidad == 0);
        }
        public int GetLowStockCount()
        {
            return ListaProductos.Count(p => p.Cantidad == 0);
        }

        public decimal GetAveragePrice()
        {
            if (ListaProductos.Count == 0)
                return 0;

            decimal total = GetTotalPrice();
            return total / ListaProductos.Count;
        }

        public decimal GetTotalPrice()
        {
            return ListaProductos.Sum(p => p.Precio ?? 0);
        }

        public List<Producto> GetAllProductos()
        {
            return ListaProductos;
        }

        public List<Producto> GetOutStockProductos()
        {
            return ListaProductos.Where(p => p.Cantidad == 0).ToList();
        }

        public int CountAllProductos()
        {
            return ListaProductos.Count;
        }

        public void AddProducto(Producto nuevoProducto)
        {
            nuevoProducto.Id = ListaProductos.Count + 1; // Asignar un ID único basado en la cantidad actual de productos
            ListaProductos.Add(nuevoProducto);
        }

        public void CrearProducto(string nombre, string categoria, decimal precio, int cantidad)

        {
            Producto nuevoProducto = new Producto
            {
                Nombre = nombre,
                Categoria = categoria,
                Precio = precio,
                Cantidad = cantidad
            };

            AddProducto(nuevoProducto);
        }

        */
    }
}