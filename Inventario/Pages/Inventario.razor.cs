using Microsoft.AspNetCore.Components;
using SgiMadsi.Shared.Domain;
using SgiMadsi.Shared.Interfaces;
using SgiMadsi.Shared.Services;

namespace SgiMadsi.Inventario.Pages;
public partial class Inventario
{
    [Inject]
    private IProductoServices ProductoServices { get; set; } = default!;
    
    private List<Producto> _productos = new();
    
    protected override async Task OnInitializedAsync()
    {
        _productos = await ProductoServices.ObtenerProductosAsync();
    }
    
}

