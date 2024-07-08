namespace ObligatorioProgramacion3.Models
{
    public class RutinaEjercicio
    {
        public int IdRutina { get; set; } // Propiedad para la clave foránea de la rutina

        public int IdEjercicio { get; set; } // Propiedad para la clave foránea del ejercicio

        public string? Nombre { get; set; } // Propiedad opcional para el nombre del RutinaEjercicio

        public Rutina Rutina { get; set; } // Propiedad de navegación para la rutina

        public Ejercicio Ejercicio { get; set; } // Propiedad de navegación para el ejercicio
    }
}
