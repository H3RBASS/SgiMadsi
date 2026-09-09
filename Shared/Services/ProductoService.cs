using SgiMadsi.Shared.Domain;
using SgiMadsi.Shared.Interfaces;
using Supabase;

namespace SgiMadsi.Shared.Services
{
    public class ProductoService : IProductoServices
    {
        private readonly Client _client;
        // Cache para almacenar los productos obtenidos del servicio
        private List<Producto> _productosCache = new();

        public ProductoService(Client client)
        {
            _client = client;
        }

        public async Task<List<Producto>> ObtenerProductosAsync()
        {
            // Verificamos si ya tenemos productos en la cache
            if (_productosCache.Any())
            {
                return _productosCache;
            }
            // Si no tenemos productos en la cache, obtenemos los productos del servicio
            var response = await _client
            .From<Producto>()
            .Get();
            // Almacenamos los productos obtenidos en la cache
            _productosCache = response.Models;
            // Devolvemos los productos obtenidos del servicio
            return response.Models;
        }

        public async Task<Producto?> ObtenerProductoPorIdAsync(int id)
        {
            var producto = await _client
            .From<Producto>()
            .Where(p => p.Id == id)
            .Get();

            return producto.Models.FirstOrDefault();
        }


        public async Task CrearProductoAsync(Producto producto)
        {
            try
            {
                await _client.From<Producto>()
                    .Insert(producto);
                
                _productosCache.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear el producto: {ex.Message}", ex);
            }
        }

        public async Task EditarProductoAsync(Producto producto)
        {
            await _client.From<Producto>()
                .Where(p => p.Id == producto.Id)
                .Update(producto);

            _productosCache.Clear();
        }

        public async Task EliminarProductoAsync(int id)
        {
            await _client.From<Producto>()
                .Where(p => p.Id == id)
                .Delete();

            _productosCache.Clear();
        }
        //metodo provisional eliminar cuando se implementen procedimientos en la BD
        public async Task<int> ObtenerProductoSPC()
        {
            var response = await _client.Rpc<int>(
                "obtproductosspc",
                null
            );

            return response;
        }

    }
}