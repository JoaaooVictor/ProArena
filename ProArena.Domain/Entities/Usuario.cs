using ProArena.Domain.Enums;

namespace ProArena.Domain.Entities
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string? Nome { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public TipoUsuarioEnum TipoUsuario { get; set; }
    }
}
