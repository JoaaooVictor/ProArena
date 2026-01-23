using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProArena.Domain.Entities;

namespace ProArena.Infrastructure.Database.ConfigModels
{
    public class UsuarioConfig : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder
                .HasKey(u => u.UsuarioId);

            builder
                .Property(u => u.UsuarioId)
                .ValueGeneratedOnAdd();

            builder
                .Property(u => u.Nome)
                .HasMaxLength(50);

            builder
                .Property(u => u.Email)
                .HasMaxLength(50);

            builder
                .Property(u => u.Senha)
                .HasMaxLength(100);
        }
    }
}
