using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObligatorioProgramacion3.Models
{
    public class Ejercicio
    {
        // Clave primaria generada automáticamente por la base de datos
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Nombre del ejercicio, con etiqueta para mostrar en formularios
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Por favor complete este campo.")]
        public string Nombre { get; set; }

        // Clave foránea opcional que se refiere a la máquina asociada
        public int? MaquinaId { get; set; }

        // Máquina asociada, definida como una relación opcional
        public Maquina? Maquina { get; set; }

        // Colección de RutinaEjercicio que asocia ejercicios con rutinas
        public ICollection<RutinaEjercicio>? RutinasEjercicios { get; set; }
    }
}
