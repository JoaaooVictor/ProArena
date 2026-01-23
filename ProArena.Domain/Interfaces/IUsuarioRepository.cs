
using ProArena.Domain.Entities;

namespace ProArena.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task RegistraUsuario(Usuario usuario);
        Task<Usuario?> BuscaUsuarioPorEmail(string email);
        Task<Usuario?> BuscaUsuarioPorId(int id);
    }
}
