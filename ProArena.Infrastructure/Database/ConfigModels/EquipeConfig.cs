using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProArena.Domain.Entities;

namespace ProArena.Infrastructure.Database.ConfigModels
{
    public class EquipeConfig : IEntityTypeConfiguration<Equipe>
    {
        public void Configure(EntityTypeBuilder<Equipe> builder)
        {
            builder
                .HasKey(e => e.EquipeId);

            builder
                .Property(e => e.EquipeId)
                .ValueGeneratedOnAdd();

            builder
                .HasMany(e => e.Jogadores)
                .WithMany(j => j.Equipes);

            builder
                .Property(e => e.Nome)
                .HasMaxLength(100);

            builder
                .HasMany(e => e.Jogadores)
                .WithMany(j => j.Equipes);
        }
    }
}
