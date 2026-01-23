using Microsoft.EntityFrameworkCore;
using ProArena.Domain.Entities;
using System.Security.Cryptography;

namespace ProArena.Infrastructure.Data.Context
{
    public class ProArenaContext : DbContext
    {
        public ProArenaContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProArenaContext).Assembly);
        }
        public DbSet<Campeonato> Campeonatos { get; set; }
        public DbSet<Jogador> Jogadores { get; set; }
        public DbSet<Partida> Partidas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Equipe> Equipes { get; set; }
    }
}
