using SgiMadsi.Shared.Domain;
using SgiMadsi.Shared.Interfaces;
using Supabase;
using static Supabase.Postgrest.Constants;

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

    public async Task<List<Venta>> ObtenerVentasPorFechaAsync(DateTime desde, DateTime hasta)
    {
        var response = await _client
            .From<Venta>()
            .Filter("created_at", Operator.GreaterThanOrEqual, desde.ToString("O"))
            .Filter("created_at", Operator.LessThanOrEqual, hasta.ToString("O"))
            .Get();

        return response.Models;
    }
}