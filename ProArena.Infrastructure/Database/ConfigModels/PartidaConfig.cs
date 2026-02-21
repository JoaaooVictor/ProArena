using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProArena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProArena.Infrastructure.Database.ConfigModels
{
    public class PartidaConfig : IEntityTypeConfiguration<Partida>
    {
        public void Configure(EntityTypeBuilder<Partida> builder)
        {
            builder
                .HasKey(p => p.PartidaId);

            builder
                .Property(e => e.PartidaId)
                .ValueGeneratedOnAdd();

            builder.HasOne(p => p.Campeonato)
               .WithMany(c => c.Partidas)
               .HasForeignKey(p => p.CampeonatoId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
