using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObligatorioProgramacion3.Models
{
    public class Local
    {
        // Clave primaria de la tabla, se genera automáticamente
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Nombre del local
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Por favor complete este campo.")]
        public string Nombre { get; set; }

        // Identificador de la ciudad a la que pertenece el local
        [Display(Name = "Ciudad")]
        [ForeignKey("CiudadId")]
        public int CiudadId { get; set; }
        public Ciudad? Ciudad { get; set; }

        // Dirección del local
        [Display(Name = "Direccion")]
        [Required(ErrorMessage = "Por favor complete este campo.")]
        public string Direccion { get; set; }

        // Teléfono del local, solo permite números
        [Display(Name = "Telefono")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El teléfono solo puede contener números.")]
        [Required(ErrorMessage = "Por favor complete este campo.")]
        public string Telefono { get; set; }

        // Identificador del responsable del local
        [Display(Name = "Responsable")]
        [ForeignKey("ResponsableId")]
        public int ResponsableId { get; set; }
        public Responsable? Responsable { get; set; }

        // Colección de máquinas asociadas al local
        public ICollection<Maquina>? Maquinas { get; set; }
    }
}
