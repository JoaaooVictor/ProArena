using ProArena.Domain.Entities;

namespace ProArena.Domain.Interfaces
{
    public interface IEquipeRepository
    {
        Task RegistraEquipe(Equipe equipe);
        Task AtualizaEquipe(Equipe equipe);
        Task<Equipe?> BuscaPorId(int id);
        Task<List<Equipe>> BuscaTodos();
        Task<List<Equipe>> BuscaPorCampeonato(int campeonatoId);
        Task Remove(int id);
    }
}
