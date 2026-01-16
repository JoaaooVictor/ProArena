using ProArena.Domain.Entities;

namespace ProArena.Domain
{
    public interface ICampeonatoRepository
    {
        Task<Campeonato?> BuscaCampeonatoPorId(int id);
        Task<List<Campeonato>> BuscaTodosCampeonatos();
        Task AdicionaCampeonato(Campeonato campeonato);
        Task AtualizaCampeonato(Campeonato campeonato);
    }
}
