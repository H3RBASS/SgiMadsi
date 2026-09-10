using Microsoft.AspNetCore.Components;
using SgiMadsi.Ventas.Services;

namespace SgiMadsi.Ventas.Pages;

public partial class Ventas : ComponentBase
{
    [Inject]
    private IVentasService VentasService { get; set; } = default!;

    private List<VentasTabla> VentasList { get; set; } = new();

    private DateTime FechaSeleccionada { get; set; } = DateTime.Today;

    protected override async Task OnInitializedAsync()
    {
        await CargarVentas();
    }

    private async Task CargarVentas()
    {
        VentasList = await VentasService.ObtenerVentas(FechaSeleccionada);
    }

    private async Task OnFechaChanged()
    {
        await CargarVentas();
    }

    private decimal TotalEfectivo =>
        VentasList.Where(x => x.MetodoPago == "Efectivo").Sum(x => x.PrecioProducto);

    private decimal TotalQR =>
        VentasList.Where(x => x.MetodoPago == "QR").Sum(x => x.PrecioProducto);

    private decimal Total => VentasList.Sum(x => x.PrecioProducto);
}
