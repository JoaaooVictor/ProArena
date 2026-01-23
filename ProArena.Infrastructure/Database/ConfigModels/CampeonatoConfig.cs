using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProArena.Domain.Entities;

namespace ProArena.Infrastructure.Data.ConfigModels
{
    public class CampeonatoConfig : IEntityTypeConfiguration<Campeonato>
    {
        public void Configure(EntityTypeBuilder<Campeonato> builder)
        {
            builder
                .HasKey(c => c.CampeonatoId);

            builder
                .Property(c => c.Descricao)
                .HasMaxLength(100);

            builder
                .Property(c => c.CampeonatoId)
                .ValueGeneratedOnAdd();

            builder.HasMany(c => c.Equipes)
               .WithOne(e => e.Campeonato)
               .HasForeignKey(e => e.CampeonatoId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Partidas)
                   .WithOne(p => p.Campeonato)
                   .HasForeignKey(p => p.CampeonatoId);
        }
    }
}
