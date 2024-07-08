using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObligatorioProgramacion3.Models
{
    // Clase Responsable que hereda de la clase Persona
    public class Responsable : Persona
    {
        // Propiedad para el local asociado al responsable

        [Display(Name = "Local")]
        public Local? Local { get; set; }
    }
}
