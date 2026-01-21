using ProArena.Application.DTOs;
using ProArena.Application.Utils;
using ProArena.Domain.Entities;

namespace ProArena.Application.Interfaces
{
    public interface ICampeonatoService
    {
        Task<ResultadoOperacao> BuscaCampeonatoPorId(int id);
        Task<ResultadoOperacao> BuscaTodosCampeonatos();
        Task<ResultadoOperacao> AdicionaCampeonato(RegistraCampeonatoDTO registraCampeonatoDTO);
    }
}
