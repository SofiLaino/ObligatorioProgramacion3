using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ObligatorioProgramacion3.Models
{
    public class TipoSocio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Clave primaria con generación automática de identidad

        [Display(Name = "Nombre tipo socio")]
        public string Nombre { get; set; } // Propiedad para el nombre del tipo de socio
    }
}
