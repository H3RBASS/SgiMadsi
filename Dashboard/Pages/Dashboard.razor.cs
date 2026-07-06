using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;
using SgiMadsi.Shared.Domain;
using SgiMadsi.Shared.Services;
using SgiMadsi.Shared.Interfaces;

namespace SgiMadsi.Dashboard.Pages;
public partial class Dashboard
{
    // Inyectamos el servicio de productos
    [Inject]
    private IProductoServices ProductoServices { get; set; } = default!;

    //lista donde se guardaran los productos obtenidos del servicio
    private List<Producto> _productos = new();

    //metodo que se ejecuta al inicializar el componente
    protected override async Task OnInitializedAsync()
    {
        _productos = await ProductoServices.ObtenerProductosAsync();
    }
    private List<Producto> ProductosFueraStock =>
        _productos.Where(p => p.Stock == 0).ToList();

    private List<Producto> ProductosBajoStock =>
        _productos.Where(p => p.Stock > 0 && p.Stock <= 10).ToList();



    // Inyectamos el servicio de productos
    [Inject]
    private ProductoService servicioProductos { get; set; } = default!;

    //variable para controlar la visibilidad del modal
    public bool MostrarModal { get; set; }
    private bool AlertaVentana {get; set;} = false;

    private Producto ProductosForm = new Producto();

    //metodos
    // protected void Guardar()
    // {
    //        servicioProductos.CrearProducto(
    //         ProductosForm.Nombre,
    //         ProductosForm.Categoria,
    //         ProductosForm.Precio!.Value,
    //         ProductosForm.Cantidad
    //     );

    //     // Limpiamos el formulario
    //     LimpiarFormulario();
    //     CerrarModal();
    // }

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
    


