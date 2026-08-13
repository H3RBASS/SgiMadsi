using SgiMadsi.Shared.Domain;
using SgiMadsi.Shared.Interfaces;
using Supabase;

namespace SgiMadsi.Shared.Services;

public class VentaService : IVentaServices
{
        private readonly Client _client;

        public VentaService(Client client)
        {
            _client = client;
        }

    public async Task<Venta?> CrearVentaAsync(Venta venta)
    {
        var response = await _client
            .From<Venta>()
            .Insert(venta);
        
        return response.Models.FirstOrDefault();
    }

    public async Task<List<Venta>> ObtenerVentasAsync()
    {
        var response = await _client
            .From<Venta>()
            .Get();
        
        return response.Models;
    }

}