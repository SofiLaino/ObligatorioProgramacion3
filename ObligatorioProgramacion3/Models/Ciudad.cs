using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ObligatorioProgramacion3.Models
{
    public class Ciudad
    {
        // Clave primaria generada automáticamente por la base de datos
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Nombre de la ciudad, con etiqueta para mostrar en formularios
        [Display(Name = "Nombre ciudad")]
        public string Nombre { get; set; }

        // Colección de locales asociados a la ciudad
        public ICollection<Local> Locales { get; set; }
    }
}
