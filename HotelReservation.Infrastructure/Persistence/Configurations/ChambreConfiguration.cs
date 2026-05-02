using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Infrastructure.Persistence.Configurations
{
    public class ChambreConfiguration : IEntityTypeConfiguration<Chambre>
    {
        public void Configure(EntityTypeBuilder<Chambre> builder)
        {
            builder.ToTable("Chambres" , t=>
            {
                t.HasCheckConstraint("Ck_Chambre_CapaciteAccueil", "CapaciteAccueil >= 1 AND CapaciteAccueil <= 5");
            });

            builder.HasKey(c => c.Id);

            builder.Property(c => c.NumChambre)
                  .IsRequired();

            builder.HasIndex(c => c.NumChambre)
                  .IsUnique();

            builder.Property(c => c.Type)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(c => c.Etage)
                .IsRequired();

            builder.Property(c => c.CapaciteAccueil)
                .IsRequired();

            builder.Property(c => c.Description)
                .HasMaxLength(500);

            builder.Property(c=> c.Statut)
                .IsRequired()
                .HasConversion<string>()
                .HasDefaultValue(StatutChambre.Disponible);

            //Relation Many To Many
            builder.HasMany(c => c.Equipements)
                .WithMany(e => e.Chambres);









        }
    }
}
