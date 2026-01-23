using ProArena.Application.DTOs.Usuarios;
using ProArena.Application.Utils;
using ProArena.Domain.Entities;

namespace ProArena.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<ResultadoOperacao> RegistraUsuario(RegistraUsuarioDTO registraUsuarioDTO);
        Task<Usuario?> BuscaUsuarioPorEmail(string email);
        Task<Usuario?> BuscaUsuarioPorId(int id);
    }
}
