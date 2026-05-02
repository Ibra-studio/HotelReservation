using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Infrastructure.Persistence.Configurations
{
    public class TarifConfiguration : IEntityTypeConfiguration<Tarif>
    {
        public void Configure(EntityTypeBuilder<Tarif> builder)
        {
            builder.ToTable("Tarifs");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.TypeChambre).IsRequired().HasConversion<string>();
            builder.Property(t => t.Saison).IsRequired().HasConversion<string>();
            builder.Property(t => t.PrixParNuit).IsRequired().HasColumnType("decimal(10,2)");

            //Tarif unique pour chaque combinaison de TypeChambre et Saison
            builder.HasIndex(t => new { t.TypeChambre, t.Saison }).IsUnique();


        }

    }
}
