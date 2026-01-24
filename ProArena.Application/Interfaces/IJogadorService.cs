using ProArena.Application.DTOs;
using ProArena.Application.Utils;
using ProArena.Domain.Entities;

namespace ProArena.Application.Interfaces
{
    public interface IJogadorService
    {
        Task<ResultadoOperacao> AtualizaJogador(AtualizaJogadorDTO atualizaJogadorDTO);
        Task<ResultadoOperacao> RegistraJogador(RegistraJogadorDTO registraJogadorDTO);
        Task<ResultadoOperacao> BuscaJogadorPorId(int id);
    }
}
