using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObligatorioProgramacion3.Models
{
    public class Persona
    {
        // Clave primaria de la tabla, se genera automáticamente
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Nombre de la persona
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Por favor complete este campo.")]
        public string Nombre { get; set; }

        // Apellido de la persona
        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "Por favor complete este campo.")]
        public string Apellido { get; set; }

        // Teléfono de la persona, solo puede contener números
        [Display(Name = "Telefono")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El teléfono solo puede contener números.")]
        [Required(ErrorMessage = "Por favor complete este campo.")]
        public string Telefono { get; set; }
    }
}
