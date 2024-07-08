using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObligatorioProgramacion3.Models
{
    public class Maquina
    {
        // Clave primaria de la tabla, se genera automáticamente
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Nombre de la máquina
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Por favor complete este campo.")]
        public string Nombre { get; set; }

        // Relación con el local asociado
        [ForeignKey("LocalAsociadoId")]
        public Local? LocalAsociado { get; set; }
        public int LocalAsociadoId { get; set; }

        // Fecha de compra de la máquina
        [Required(ErrorMessage = "Por favor complete este campo.")]
        [DataType(DataType.Date)]
        public DateTime FechaCompra { get; set; }

        // Precio de compra de la máquina
        [Required(ErrorMessage = "Por favor complete este campo.")]
        public int PrecioCompra { get; set; }

        // Vida útil de la máquina en años
        [Required(ErrorMessage = "Por favor complete este campo.")]
        public int VidaUtil { get; set; }

        // Relación con el tipo de máquina
        [ForeignKey("TipoMaquinaId")]
        public TipoMaquina? TipoMaquina { get; set; }
        public int TipoMaquinaId { get; set; }

        // Indica si la máquina está disponible
        public bool Disponible { get; set; }
    }
}
