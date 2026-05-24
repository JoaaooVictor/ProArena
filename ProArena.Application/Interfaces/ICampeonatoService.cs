using ProArena.Application.DTOs.Campeonatos;
using ProArena.Application.Utils;
using ProArena.Domain.Entities;

namespace ProArena.Application.Interfaces
{
    public interface ICampeonatoService
    {
        Task<ResultadoOperacao> BuscaCampeonatoPorId(int id);
        Task<ResultadoOperacao> BuscaTodosCampeonatos();
        Task<ResultadoOperacao> AdicionaCampeonato(RegistraCampeonatoDTO registraCampeonatoDTO);
        Task<ResultadoOperacao> IniciaCampeonato(int campeonatoId);
        Task<ResultadoOperacao> BuscaChaveamento(int campeonatoId);
    }
}
