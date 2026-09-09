using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;
using SgiMadsi.Shared.Domain;
using SgiMadsi.Shared.Services;
using SgiMadsi.Shared.Interfaces;
using SgiMadsi.Dashboard.Services;
using SgiMadsi.Dashboard.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace SgiMadsi.Dashboard.Pages;
public partial class Dashboard
{
    // Inyectamos el servicio de productos
    [Inject]
    private IProductoServices ProductoServices { get; set; } = default!;

    [Inject]
    private IProveedoresServices ProveedorServices { get; set; } = default!;

    [Inject]
    private IDashService DashService { get; set; } = default!;

    //variables
    private bool _procesando;
    private bool _procesandoProveedor;
    
    private bool _mensajeConfirmacion;
    private bool _mensajeConfirmacionProveedores;

    //variable para guardar la imagen temporalmente
    private IBrowserFile? _archivoSeleccionado;

    //lista donde se guardaran los productos obtenidos del servicio
    private List<Producto> _productos = new();

    // DTO con las ventas del dia ensambladas
    private List<VentaDelDiaDto> _ventasDelDia = new();

    private HashSet<int> _ventasExpandidas = new();

    //objetos vacios para crear productos y despues anadirlos a la BD
    private Producto _nuevoProducto = new();
    private Proveedores _nuevoProveedor = new();
    private int _ObtProductosSPC;

    //metodo que se ejecuta al inicializar el componente
    protected override async Task OnInitializedAsync()
    {
        _productos = await ProductoServices.ObtenerProductosAsync();
        _ventasDelDia = await DashService.ObtenerVentasDelDiaAsync();
        _ObtProductosSPC = await ProductoServices.ObtenerProductoSPC();
    }
    private List<Producto> ProductosFueraStock =>
        _productos.Where(p => p.Stock == 0).ToList();

    private List<Producto> ProductosBajoStock =>
        _productos.Where(p => p.Stock > 0 && p.Stock <= 10).ToList();


    private static string FormatearFechaBolivia(DateTime fechaLocal)
    {
        return fechaLocal.ToString("HH:mm");
    }

    private void ToggleVenta(int ventaId)
    {
        if (!_ventasExpandidas.Remove(ventaId))
            _ventasExpandidas.Add(ventaId);
    }
    //variable para controlar la visibilidad del modal
    private bool MostrarModal { get; set; }
    private bool MostrarVentanaProveedor {get; set;}

    public async Task GuardarProducto()
    {
        if(_procesando)
    
            return;
             _procesando = true;
        
        try
        {
            _nuevoProducto.Nombre = LimpiarTexto(_nuevoProducto.Nombre);
            _nuevoProducto.Subcategoria = LimpiarTexto(_nuevoProducto.Subcategoria);
            await ProductoServices.CrearProductoAsync(_nuevoProducto);
        }

        finally
        {
            // Limpiamos el formulario
            LimpiarFormulario();
            //obtenemos los datos actualizados
            _productos = await ProductoServices.ObtenerProductosAsync();
            //habilitamos el boton de guardar
            _procesando = false;
            _mensajeConfirmacion = true;
            //indicar que algun componente cambio y blazor vuelva a renderizar
            StateHasChanged();
            await Task.Delay(2000);
            StateHasChanged();
            _mensajeConfirmacion = false;
        }
    }

    private void SeleccionarImagen(InputFileChangeEventArgs e)
    {
        _archivoSeleccionado = e.File;
    }
    private void EliminarArchivo()
    {
        _archivoSeleccionado = null;
    }
    private async Task GuardarProveedor()
    {
        if(_procesandoProveedor)
            return;
        
        _procesandoProveedor = true;

        try
        {
            if (_archivoSeleccionado is not null)
            {
                var url = await ProveedorServices.SubirImagenAsync(_archivoSeleccionado);

                _nuevoProveedor.Imagen = url;
            }
            await ProveedorServices.CrearProveedorAsync(_nuevoProveedor);
        }
        
        finally
        {
            LimpiarFormulario();
            _procesandoProveedor = false;
            _mensajeConfirmacionProveedores = true;
            StateHasChanged();
            await Task.Delay(2000);
            StateHasChanged();
            _mensajeConfirmacionProveedores = false;
        }
    }

    private static string LimpiarTexto(string texto)
    {
        // La cadena de ej es "oLa maximus  Antigrasa"
        // Limpiar espacios multiples
        var resultado = texto.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        //en resultado tenemos el arreglo con las palabras ["oLa ", "maximus ","Antigrasa"]
        for (int i = 0 ; i < resultado.Length; i++)
        {
            //obtenemos la primer palabra del arreglo
            string palabraActual = resultado[i];
            //obtenemos la primer letra
            char primerLetra = char.ToUpper(palabraActual[0]);
            //obtenemos el resto de la palabra desde X posicion en adelante y en minusculas
            string restoPalabra = palabraActual.Substring(1).ToLower();
            //creamos la cadena contatenando las partes
            resultado[i] = $"{primerLetra}{restoPalabra}";
        }
        //Retornamos la palabra uniendo
        return string.Join(" ", resultado);
    } 

    protected void AbrirVentanaProveedor()
    {
        LimpiarFormulario();
        MostrarVentanaProveedor = true;
    }
    
    protected void CerrarVentanaProveedor()
    {
        LimpiarFormulario();
        EliminarArchivo();
        MostrarVentanaProveedor = false;
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
        _nuevoProveedor = new Proveedores();
    }

}   