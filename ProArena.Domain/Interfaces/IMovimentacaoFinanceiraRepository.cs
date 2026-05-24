using ProArena.Domain.Entities;

namespace ProArena.Domain.Interfaces
{
    public interface IMovimentacaoFinanceiraRepository
    {
        Task<List<MovimentacaoFinanceira>> BuscaTodosAsync();
        Task<List<MovimentacaoFinanceira>> BuscaPorPeriodoAsync(DateTime inicio, DateTime fim);
        Task<MovimentacaoFinanceira?> BuscaPorIdAsync(int id);
        Task RegistraAsync(MovimentacaoFinanceira movimentacao);
        Task AtualizaAsync(MovimentacaoFinanceira movimentacao);
        Task RemoveAsync(MovimentacaoFinanceira movimentacao);
    }
}
