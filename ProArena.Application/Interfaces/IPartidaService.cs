using ProArena.Application.DTOs;
using ProArena.Application.Utils;

namespace ProArena.Application.Interfaces
{
    public interface IPartidaService
    {
        Task<ResultadoOperacao> RegistraPartida(RegistraPartidaDTO dto);
        Task<ResultadoOperacao> AtualizaPartida(AtualizaPartidaDTO dto);
        Task<ResultadoOperacao> BuscaPorId(int id);
        Task<ResultadoOperacao> BuscaPorCampeonato(int campeonatoId);
        Task<ResultadoOperacao> BuscaTodos();
        Task<ResultadoOperacao> Remove(int id);
    }
}
