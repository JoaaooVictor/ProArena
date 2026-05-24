using Microsoft.EntityFrameworkCore;
using ProArena.Domain.Entities;
using ProArena.Domain.Interfaces;
using ProArena.Infrastructure.Data.Context;

namespace ProArena.Infrastructure.Repositories
{
    public class MovimentacaoFinanceiraRepository : IMovimentacaoFinanceiraRepository
    {
        private readonly ProArenaContext _context;

        public MovimentacaoFinanceiraRepository(ProArenaContext context)
        {
            _context = context;
        }

        public async Task<List<MovimentacaoFinanceira>> BuscaTodosAsync()
        {
            return await _context.MovimentacoesFinanceiras
                .Include(m => m.Campeonato)
                .OrderByDescending(m => m.Data)
                .ToListAsync();
        }

        public async Task<List<MovimentacaoFinanceira>> BuscaPorPeriodoAsync(DateTime inicio, DateTime fim)
        {
            return await _context.MovimentacoesFinanceiras
                .Include(m => m.Campeonato)
                .Where(m => m.Data.Date >= inicio.Date && m.Data.Date <= fim.Date)
                .OrderByDescending(m => m.Data)
                .ToListAsync();
        }

        public async Task<MovimentacaoFinanceira?> BuscaPorIdAsync(int id)
        {
            return await _context.MovimentacoesFinanceiras
                .Include(m => m.Campeonato)
                .FirstOrDefaultAsync(m => m.MovimentacaoFinanceiraId == id);
        }

        public async Task RegistraAsync(MovimentacaoFinanceira movimentacao)
        {
            await _context.MovimentacoesFinanceiras.AddAsync(movimentacao);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizaAsync(MovimentacaoFinanceira movimentacao)
        {
            _context.MovimentacoesFinanceiras.Update(movimentacao);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(MovimentacaoFinanceira movimentacao)
        {
            _context.MovimentacoesFinanceiras.Remove(movimentacao);
            await _context.SaveChangesAsync();
        }
    }
}
