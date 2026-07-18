namespace app.microCliente.common.DTOs
{
    public class ClienteDireccionEventDto
    {
        public int ClienteId { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DireccionCompleta { get; set; } = string.Empty;
    }
}
