using Microsoft.EntityFrameworkCore;
using ProArena.Domain;
using ProArena.Domain.Entities;
using ProArena.Infrastructure.Data.Context;

namespace ProArena.Infrastructure.Repositories
{
    public class CampeonatoRepository : ICampeonatoRepository
    {
        private readonly ProArenaContext _context;
        public CampeonatoRepository(ProArenaContext context)
        {
            _context = context;
        }

        public async Task AdicionaCampeonato(Campeonato campeonato)
        {
            await _context.Campeonatos.AddAsync(campeonato);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizaCampeonato(Campeonato campeonato)
        {
            _context.Entry(campeonato).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Campeonato?> BuscaCampeonatoPorId(int id)
        {
            return await _context.Campeonatos.FirstOrDefaultAsync(c => c.CampeonatoId == id);
        }

        public async Task<List<Campeonato>> BuscaTodosCampeonatos()
        {
            return await _context.Campeonatos.ToListAsync();
        }
    }
}
