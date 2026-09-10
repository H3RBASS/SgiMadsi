namespace SgiMadsi.Ventas.Services;
using Supabase;
using static Supabase.Postgrest.Constants;

public interface IVentasService
{
    Task<List<VentasTabla>> ObtenerVentas(DateTime fecha);
}

public class VentasService : IVentasService
{
    private readonly Client _client;

    public VentasService(Client client)
    {
        _client = client;
    }

    public async Task<List<VentasTabla>> ObtenerVentas(DateTime fecha)
    {
        DateTime inicio = fecha.Date;
        DateTime fin = inicio.AddDays(1);
        var response = await _client
            .From<VentasTabla>()
            .Filter("fecha", Operator.GreaterThanOrEqual, inicio.ToString("O"))
            .Filter("fecha", Operator.LessThan, fin.ToString("O"))
            .Get();

        return response.Models;
    }
}
