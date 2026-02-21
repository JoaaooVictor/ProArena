using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProArena.Domain.Entities;

namespace ProArena.Infrastructure.Database.ConfigModels
{
    public class InscricaoConfig : IEntityTypeConfiguration<Inscricao>
    {
        public void Configure(EntityTypeBuilder<Inscricao> builder)
        {
            builder
                .HasKey(i => i.InscricaoId);
        }
    }
}
