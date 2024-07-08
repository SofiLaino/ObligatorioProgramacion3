using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObligatorioProgramacion3.Models
{
    public class Rutina
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Propiedad Id como clave primaria con generación automática

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Por favor complete este campo.")]
        public string Nombre { get; set; } // Propiedad para el nombre de la rutina

        public int TipoRutinaId { get; set; } // Propiedad para la clave foránea del tipo de rutina
        public TipoRutina? TipoRutina { get; set; } // Propiedad de navegación para el tipo de rutina

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "Por favor complete este campo.")]
        public string Descripcion { get; set; } // Propiedad para la descripción de la rutina

        [NotMapped]
        public decimal? PromedioCalif { get; set; } // Propiedad no mapeada en la base de datos para el promedio de calificaciones

        public ICollection<RutinaEjercicio>? RutinasEjercicios { get; set; } // Colección de ejercicios asociados a la rutina
        public ICollection<RutinaSocio>? RutinasSocios { get; set; } // Colección de socios asociados a la rutina
    }
}
