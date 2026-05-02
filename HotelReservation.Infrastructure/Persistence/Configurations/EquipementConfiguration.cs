using HotelReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Infrastructure.Persistence.Configurations
{
    public class EquipementConfiguration :IEntityTypeConfiguration<Equipement>
    {
        public void Configure(EntityTypeBuilder<Equipement> builder)
        {
            builder.ToTable("Equipements");

            builder.HasKey(e => e.Id);

            builder.Property(e=> e.Nom)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(e => e.Nom)
                .IsUnique();


        }
    }
}
