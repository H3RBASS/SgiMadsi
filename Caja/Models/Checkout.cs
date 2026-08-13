using System.ComponentModel.DataAnnotations;

namespace SgiMadsi.Caja.Models;

public class Checkout
{
    [Required(ErrorMessage = "Selecciona un método de pago.")]
    public string MetodoPago { get; set; } = string.Empty;
    [Range(0, double.MaxValue, ErrorMessage = "El descuento no puede ser negativo.")]
    public decimal Descuento { get; set; } = 0;
}