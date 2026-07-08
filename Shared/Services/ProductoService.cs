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

    }
}