using Microsoft.EntityFrameworkCore;
using ProArena.Domain.Entities;
using ProArena.Domain.Interfaces;
using ProArena.Infrastructure.Data.Context;

namespace ProArena.Infrastructure.Repositories
{
    public class PartidaRepository : IPartidaRepository
    {
        private readonly ProArenaContext _context;

        public PartidaRepository(ProArenaContext context)
        {
            _context = context;
        }

        public async Task RegistraPartida(Partida partida)
        {
            await _context.Partidas.AddAsync(partida);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizaPartida(Partida partida)
        {
            _context.Entry(partida).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Partida?> BuscaPorId(int id)
        {
            return await _context.Partidas
                .Include(p => p.Equipes)
                .Where(p => p.PartidaId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Partida>> BuscaPorCampeonato(int campeonatoId)
        {
            return await _context.Partidas
                .Include(p => p.Equipes)
                .Where(p => p.CampeonatoId == campeonatoId)
                .OrderBy(p => p.DataHora)
                .ToListAsync();
        }

        public async Task<List<Partida>> BuscaTodos()
        {
            return await _context.Partidas
                .Include(p => p.Equipes)
                .OrderBy(p => p.DataHora)
                .ToListAsync();
        }

        public async Task Remove(int id)
        {
            var partida = await BuscaPorId(id);
            if (partida != null)
            {
                _context.Partidas.Remove(partida);
                await _context.SaveChangesAsync();
            }
        }
    }
}
