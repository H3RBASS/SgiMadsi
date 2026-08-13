using SgiMadsi.Shared.Domain;
using SgiMadsi.Shared.Interfaces;
using Supabase;

namespace SgiMadsi.Shared.Services;

public class DetalleVentaService : IDetalleVentaServices
{
    private readonly Client _client;
    
    public DetalleVentaService(Client client)
    {
        _client = client;
    }
    
    public async Task<DetalleVenta?> CrearDetalleVentaAsync(DetalleVenta detalleVenta)
    {
        var response = await _client
            .From<DetalleVenta>()
            .Insert(detalleVenta);
        
        return response.Models.FirstOrDefault();
    }
    
    public async Task<List<DetalleVenta>> ObtenerDetallesVentaAsync(int ventaId)
    {
        var response = await _client
            .From<DetalleVenta>()
            .Where(d => d.VentaId == ventaId)
            .Get();
        
        return response.Models;
    }
    
}