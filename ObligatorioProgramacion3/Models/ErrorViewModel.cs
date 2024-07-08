namespace ObligatorioProgramacion3.Models
{
    public class ErrorViewModel
    {
        // Identificador de la solicitud, puede ser nulo
        public string? RequestId { get; set; }

        // Propiedad que indica si se debe mostrar el RequestId
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
