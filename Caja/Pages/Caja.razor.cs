using Microsoft.AspNetCore.Components;
using SgiMadsi.Shared.Domain;
using SgiMadsi.Shared.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SgiMadsi.Caja.Pages
{
    public partial class Caja
    {
        [Inject]
        private IProductoServices ProductoServices { get; set; } = default!;
        private List<Producto> _productosOriginales = new();
        private List<string> Categorias { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            _productosOriginales = await ProductoServices.ObtenerProductosAsync();
            _productosOriginales = _productosOriginales.OrderBy(p => p.Subcategoria).ToList();

            Categorias = _productosOriginales.Select(p => p.Subcategoria)
            .Distinct()
            .ToList(); 
        }
        private void obtenerProductoVenta(string _producto)
        {
            
        }

        private void SumarCuenta()
        {
            if(Categorias.Any())
            {
                
            }
        }
    }

    
}
