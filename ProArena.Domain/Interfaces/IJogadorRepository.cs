using ProArena.Domain.Entities;

namespace ProArena.Domain.Interfaces
{
    public interface IJogadorRepository
    {
        Task AtualizaJogador(Jogador jogador);
        Task RegistraJogador(Jogador jogador);
        Task<Jogador?> BuscaJogadorPorId(int id);
    }
}
