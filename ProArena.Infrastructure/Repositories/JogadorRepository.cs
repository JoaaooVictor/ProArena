using Microsoft.EntityFrameworkCore;
using ProArena.Domain.Entities;
using ProArena.Domain.Interfaces;
using ProArena.Infrastructure.Data.Context;

namespace ProArena.Infrastructure.Repositories
{
    public class JogadorRepository : IJogadorRepository
    {
        private readonly ProArenaContext _context;

        public JogadorRepository(ProArenaContext context)
        {
            _context = context;
        }

        public async Task AtualizaJogador(Jogador jogador)
        {
            _context.Entry(jogador).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task RegistraJogador(Jogador jogador)
        {
            await _context.Jogadores.AddAsync(jogador);
            await _context.SaveChangesAsync();
        }

        public async Task<Jogador?> BuscaJogadorPorId(int id)
        {
            return await _context.Jogadores.Where(j => j.JogadorId == id).FirstOrDefaultAsync();
        }
    }
}
