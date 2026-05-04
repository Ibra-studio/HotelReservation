using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Infrastructure.Persistence.Configurations
{
    internal class FactureConfiguration : IEntityTypeConfiguration<Facture>
    {
        public void Configure(EntityTypeBuilder<Facture> builder)
        {
            builder.ToTable("Factures");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.MontantTotal)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(f => f.DateEmission)
                .IsRequired();

            builder.Property(f => f.MontantRemise)
                .IsRequired()
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0m);


            builder.Property(f=> f.MontantPenalitee)
                .IsRequired()
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0m);

            builder.Property(f => f.MontantNuitee)
                .IsRequired()
                .HasColumnType("decimal(10,2)");
            builder.Property(f => f.MontantServices)
                .IsRequired()
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0m);


            builder.Property(f => f.Statut)
                .IsRequired()
                .HasConversion<string>()
                .HasDefaultValue(StatutPaiement.EnAttente);


            //relation one one avec reservation

            builder.HasOne(f => f.Reservation)
                    .WithOne(r => r.Facture)
                    .HasForeignKey<Facture>(f => f.ReservationId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}