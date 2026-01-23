using Microsoft.EntityFrameworkCore;
using ProArena.Domain.Entities;
using ProArena.Domain.Interfaces;
using ProArena.Infrastructure.Data.Context;

namespace ProArena.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ProArenaContext _context;

        public UsuarioRepository(ProArenaContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> BuscaUsuarioPorEmail(string email)
        {
            return await _context.Usuarios.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<Usuario?> BuscaUsuarioPorId(int id)
        {
            return await _context.Usuarios.Where(u => u.UsuarioId == id).FirstOrDefaultAsync();
        }

        public async Task RegistraUsuario(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
