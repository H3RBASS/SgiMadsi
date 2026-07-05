// using Microsoft.AspNetCore.Components;
// using SgiMadsi.Caja.Services;
// using SgiMadsi.Shared.Domain;
// using System.Collections.Generic;
// using System.Linq;

// namespace SgiMadsi.Caja.Pages
// {
//     public partial class Caja
//     {
//         [Inject]
//         private VentaService ventaService { get; set; } = default!;

//         private List<string> Carrito = new ()
//         {
//             "Mouse",
//             "Teclado",
//             "Monitor"
//         };

//         private string busqueda = string.Empty;

//         private IEnumerable<CarritoItem> CarritoItems => 
//             ventaService.Carrito.Where(c =>
//                 string.IsNullOrWhiteSpace(busqueda) ||
//                  c.Nombre.Contains(busqueda, StringComparison.OrdinalIgnoreCase));
//     }
// }