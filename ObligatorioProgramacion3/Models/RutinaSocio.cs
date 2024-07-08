namespace ObligatorioProgramacion3.Models
{
    public class RutinaSocio
    {
        public int IdRutina { get; set; } // Propiedad para la clave foránea de la rutina

        public int IdSocio { get; set; } // Propiedad para la clave foránea del socio

        public DateTime Fecha { get; set; } // Propiedad para la fecha de asignación de la rutina al socio

        public decimal? Calificacion { get; set; } // Propiedad opcional para la calificación de la rutina

        public Socio? Socio { get; set; } // Propiedad de navegación para el socio

        public Rutina? Rutina { get; set; } // Propiedad de navegación para la rutina
    }
}
