using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProArena.Domain.Entities;

namespace ProArena.Infrastructure.Database.ConfigModels
{
    public class JogadorConfig : IEntityTypeConfiguration<Jogador>
    {
        public void Configure(EntityTypeBuilder<Jogador> builder)
        {
            builder.HasKey(j => j.JogadorId);
            builder.Property(j => j.Nome).IsRequired().HasMaxLength(100);
            builder.Property(j => j.Cpf).IsRequired().HasMaxLength(11);
        }
    }
}
