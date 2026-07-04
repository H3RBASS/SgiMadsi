using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;
using SgiMadsi.Shared.Domain;
using SgiMadsi.Shared.Services;

namespace SgiMadsi.Dashboard.Pages;
public partial class Dashboard
{
    // Inyectamos el servicio de productos
    [Inject]
    private ProductoService servicioProductos { get; set; } = default!;

    //variable para controlar la visibilidad del modal
    public bool MostrarModal { get; set; }
    private bool AlertaVentana {get; set;} = false;

    private Producto ProductosForm = new Producto();

    //metodos
    protected void Guardar()
    {
           servicioProductos.CrearProducto(
            ProductosForm.Nombre,
            ProductosForm.Categoria,
            ProductosForm.Precio!.Value,
            ProductosForm.Cantidad
        );

        // Limpiamos el formulario
        LimpiarFormulario();
        CerrarModal();
    }

    protected void AbrirModal()
    {
        LimpiarFormulario();
        MostrarModal = true;
    }

    protected void CerrarModal()
    {
        LimpiarFormulario();
        MostrarModal = false;
    }

    protected void LimpiarFormulario()
    {
        ProductosForm = new Producto();
    }
}   
    


