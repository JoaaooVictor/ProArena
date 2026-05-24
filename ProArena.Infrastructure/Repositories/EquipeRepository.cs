using Microsoft.EntityFrameworkCore;
using ProArena.Domain.Entities;
using ProArena.Domain.Interfaces;
using ProArena.Infrastructure.Data.Context;

namespace ProArena.Infrastructure.Repositories
{
    public class EquipeRepository : IEquipeRepository
    {
        private readonly ProArenaContext _context;

        public EquipeRepository(ProArenaContext context)
        {
            _context = context;
        }

        public async Task RegistraEquipe(Equipe equipe)
        {
            await _context.Equipes.AddAsync(equipe);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizaEquipe(Equipe equipe)
        {
            _context.Entry(equipe).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Equipe?> BuscaPorId(int id)
        {
            return await _context.Equipes
                .Include(e => e.Jogadores)
                .Include(e => e.Partidas)
                .Where(e => e.EquipeId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Equipe>> BuscaTodos()
        {
			return await _context.Equipes
	                .Include(e => e.Jogadores)
	                .ToListAsync();
		}

        public async Task<List<Equipe>> BuscaPorCampeonato(int campeonatoId)
        {
            return await _context.Equipes
                .Where(e => e.CampeonatoId == campeonatoId)
                .OrderBy(e => e.Nome)
                .ToListAsync();
        }

        public async Task Remove(int id)
        {
            var equipe = await BuscaPorId(id);
            if (equipe != null)
            {
                _context.Equipes.Remove(equipe);
                await _context.SaveChangesAsync();
            }
        }
    }
}
