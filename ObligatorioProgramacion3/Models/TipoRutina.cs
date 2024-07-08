using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ObligatorioProgramacion3.Models
{
    public class TipoRutina
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Clave primaria con generación automática de identidad

        [Display(Name = "Nombre tipo rutina")]
        public string Nombre { get; set; } // Propiedad para el nombre del tipo de rutina
    }
}
