namespace SgiMadsi.Ventas.Services;
using Supabase;
using static Supabase.Postgrest.Constants;

public interface IVentasService
{
    Task<List<VentasTablaBo>> ObtenerVentas(DateTime fecha);
}

public class VentasService : IVentasService
{
    private readonly Client _client;

    public VentasService(Client client)
    {
        _client = client;
    }

    public async Task<List<VentasTablaBo>> ObtenerVentas(DateTime fecha)
    {
        DateTime inicio = fecha.Date;
        DateTime fin = inicio.AddDays(1);
        var response = await _client
            .From<VentasTablaBo>()
            .Filter("fecha", Operator.GreaterThanOrEqual, inicio.ToString("yyyy-MM-ddTHH:mm:ss"))
            .Filter("fecha", Operator.LessThan, fin.ToString("yyyy-MM-ddTHH:mm:ss"))
            .Get();

        return response.Models;
    }
}
