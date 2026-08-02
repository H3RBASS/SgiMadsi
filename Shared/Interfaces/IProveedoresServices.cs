using Microsoft.AspNetCore.Components.Forms;
using SgiMadsi.Shared.Domain;

namespace SgiMadsi.Shared.Interfaces;

public interface IProveedoresServices
{
    // Task<List<Proveedores>> ObtenerProveedoresAsync();
    Task CrearProveedorAsync(Proveedores proveedor);
    Task<string> SubirImagenAsync(IBrowserFile file);
}