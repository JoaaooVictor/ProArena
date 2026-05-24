using ProArena.Domain.Entities;

namespace ProArena.Domain.Interfaces
{
    public interface IPartidaRepository
    {
        Task RegistraPartida(Partida partida);
        Task AtualizaPartida(Partida partida);
        Task<Partida?> BuscaPorId(int id);
        Task<List<Partida>> BuscaPorCampeonato(int campeonatoId);
        Task<List<Partida>> BuscaTodos();
        Task Remove(int id);
    }
}
