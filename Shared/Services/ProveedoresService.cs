using SgiMadsi.Shared.Domain;
using SgiMadsi.Shared.Interfaces;
using Supabase;

namespace SgiMadsi.Shared.Services;

public class ProveedoresService : IProveedoresServices
{
    private readonly Client _cliente;

    public ProveedoresService(Client cliente)
    {
        _cliente = cliente;
    }

    public async Task CrearProveedorAsync(Proveedores proveedor)
    {
        await _cliente.From<Proveedores>()
            .Insert(proveedor);
    }
}