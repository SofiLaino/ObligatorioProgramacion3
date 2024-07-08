using System.ComponentModel.DataAnnotations;

namespace ObligatorioProgramacion3.Models
{
    public class Socio : Persona
    {
        [Display(Name = "Tipo de Socio")]
        public int TipoSocioId { get; set; } // Propiedad para la clave foránea del tipo de socio
        public TipoSocio? TipoSocio { get; set; } // Propiedad de navegación para el tipo de socio

        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        [Required(ErrorMessage = "Por favor complete este campo.")]
        public string? Email { get; set; } // Propiedad para el email con validación de formato

        [Display(Name = "Local Asociado")]
        public int LocalAsociadoId { get; set; } // Propiedad para la clave foránea del local asociado
        public Local? LocalAsociado { get; set; } // Propiedad de navegación para el local asociado

        public ICollection<RutinaSocio>? RutinasSocios { get; set; } // Propiedad de navegación para las rutinas asociadas al socio
    }
}
