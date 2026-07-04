namespace SgiMadsi.Shared.Domain
{
    public class CarritoItem
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Categoria { get; set; } = "";
        public decimal Precio { get; set; }
        public int CantidadDisponible { get; set; }  // Stock real
        public int CantidadSeleccionada { get; set; } // Cantidad a comprar

        // Propiedad calculada (solo lectura)
        public decimal Subtotal => Precio * CantidadSeleccionada;
    }
}