using SgiMadsi.Shared.Domain;

namespace SgiMadsi.Shared.Interfaces;

public interface IProveedoresServices
{
    Task CrearProveedorAsync(Proveedores proveedor);
    
}