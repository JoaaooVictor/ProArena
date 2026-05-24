using ProArena.Application.DTOs;
using ProArena.Application.Utils;

namespace ProArena.Application.Interfaces
{
    public interface IMovimentacaoFinanceiraService
    {
        Task<ResultadoOperacao> RegistraAsync(RegistraMovimentacaoFinanceiraDTO dto);
        Task<ResultadoOperacao> AtualizaAsync(AtualizaMovimentacaoFinanceiraDTO dto);
        Task<ResultadoOperacao> RemoveAsync(int id);
        Task<ResultadoOperacao> BuscaPorIdAsync(int id);
        Task<ResultadoOperacao> BuscaTodosAsync();
        Task<ResultadoOperacao> BuscaPorPeriodoAsync(DateTime inicio, DateTime fim);
        Task<ResultadoOperacao> BuscaResumoAsync(DateTime? inicio = null, DateTime? fim = null);
    }
}
