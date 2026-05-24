using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProArena.Domain.Entities;

namespace ProArena.Infrastructure.Database.ConfigModels
{
    public class MovimentacaoFinanceiraConfig : IEntityTypeConfiguration<MovimentacaoFinanceira>
    {
        public void Configure(EntityTypeBuilder<MovimentacaoFinanceira> builder)
        {
            builder.ToTable("MovimentacoesFinanceiras");
            builder.HasKey(m => m.MovimentacaoFinanceiraId);
            builder.Property(m => m.Descricao).HasMaxLength(200).IsRequired();
            builder.Property(m => m.Categoria).HasMaxLength(100);
            builder.Property(m => m.Valor).HasPrecision(18, 2);

            builder.HasOne(m => m.Campeonato)
                .WithMany()
                .HasForeignKey(m => m.CampeonatoId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
