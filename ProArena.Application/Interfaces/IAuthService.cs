using ProArena.Application.DTOs.Usuarios;
using ProArena.Application.Utils;

namespace ProArena.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ResultadoOperacao> Login(LoginUsuarioDTO loginUsuarioDTO);
        Task<bool> ValidaCredenciais(LoginUsuarioDTO loginUsuarioDTO);
        Task<string> GeraTokenJwt(string email);
        Task<string> GeraHashSenha(string senha);
        Task<bool> VerificaSenha(string senha, string hashSenha);
    }
}
