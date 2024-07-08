using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObligatorioProgramacion3.Models
{
    public class TipoMaquina
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Clave primaria con generación automática de identidad

        [Display(Name = "Nombre tipo maquina")]
        public string Nombre { get; set; } // Propiedad para el nombre del tipo de máquina
    }
}
