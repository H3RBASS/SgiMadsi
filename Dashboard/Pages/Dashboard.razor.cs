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

    //lista para crear productos
    private Producto _nuevoProducto = new();

    //metodo que se ejecuta al inicializar el componente
    protected override async Task OnInitializedAsync()
    {
        _productos = await ProductoServices.ObtenerProductosAsync();
    }
    private List<Producto> ProductosFueraStock =>
        _productos.Where(p => p.Stock == 0).ToList();

    private List<Producto> ProductosBajoStock =>
        _productos.Where(p => p.Stock > 0 && p.Stock <= 10).ToList();

    //variable para controlar la visibilidad del modal
    public bool MostrarModal { get; set; }
    private bool AlertaVentana {get; set;} = false;

    public async Task GuardarProducto()
    {
        await ProductoServices.CrearProductoAsync(_nuevoProducto);
        // Limpiamos el formulario
        LimpiarFormulario();
        _productos = await ProductoServices.ObtenerProductosAsync();
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
        _nuevoProducto = new Producto();
    }
}   
    


