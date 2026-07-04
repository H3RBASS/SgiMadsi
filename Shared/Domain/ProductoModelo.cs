using System.ComponentModel.DataAnnotations;

namespace SgiMadsi.Shared.Domain;

        // Clase Producto
        public class Producto
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "El nombre es obligatorio")]
            public string Nombre { get; set; } = "";
            
            [Required(ErrorMessage = "La categoría es obligatoria")]
            public string Categoria { get; set; } = "";

            [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
            [Required(ErrorMessage = "El precio es obligatorio")]
            public decimal? Precio { get; set; }

            [Range(0, int.MaxValue, ErrorMessage = "La cantidad no puede ser negativa")]
            public int Cantidad { get; set; }
            public int MinStock { get; set; }
        }