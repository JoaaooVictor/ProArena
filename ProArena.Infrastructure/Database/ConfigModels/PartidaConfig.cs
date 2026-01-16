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

            builder
                .Property(p => p.ResultadoEquipeB)
                .HasMaxLength(3);

            builder
                .Property(p => p.ResultadoEquipeA)
                .HasMaxLength(3);

            builder
                .HasOne(p => p.EquipeA)
                .WithMany()
                .HasForeignKey(p => p.EquipeAId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.EquipeB)
                .WithMany()
                .HasForeignKey(p => p.EquipeBId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
