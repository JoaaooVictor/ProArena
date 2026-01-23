namespace ProArena.Application.DTOs.Usuarios
{
    public class RegistraUsuarioDTO
    {
        public string? Nome { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}
