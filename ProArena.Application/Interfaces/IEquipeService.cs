using ProArena.Application.DTOs;
using ProArena.Application.Utils;

namespace ProArena.Application.Interfaces
{
    public interface IEquipeService
    {
        Task<ResultadoOperacao> RegistraEquipe(RegistraEquipeDTO dto);
        Task<ResultadoOperacao> AtualizaEquipe(AtualizaEquipeDTO dto);
        Task<ResultadoOperacao> BuscaPorId(int id);
        Task<ResultadoOperacao> BuscaTodos();
        Task<ResultadoOperacao> Remove(int id);
    }
}
