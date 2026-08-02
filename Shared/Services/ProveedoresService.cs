using Microsoft.AspNetCore.Components.Forms;
using SgiMadsi.Shared.Domain;
using SgiMadsi.Shared.Interfaces;
using Supabase;
using System.IO;

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

    public async Task<string> SubirImagenAsync(IBrowserFile file)
    {
        // Generar un nombre único para evitar que dos imágenes tengan el mismo nombre
        var nombreArchivo = $"{Guid.NewGuid()}{Path.GetExtension(file.Name)}";   
        //leer archivo
        using var stream = file.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024);

        using var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);

        var bytes = memoryStream.ToArray();

        //subir a SUPABASE
        await _cliente.Storage
        .From("Proveedores")
        .Upload(bytes, nombreArchivo);

        //obtener URL publica
        var url = _cliente.Storage
        .From("Proveedores")
        .GetPublicUrl(nombreArchivo);

        return url;
    }
}