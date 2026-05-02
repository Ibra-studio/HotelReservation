using HotelReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Infrastructure.Persistence.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations" , t=>
            {
                t.HasCheckConstraint("CK_Reservations_NombrePersonnes", "[NombrePersonnes] >= 1");
            });

            builder.HasKey(r => r.Id);

            builder.Property(r => r.DateArrivee)
                .IsRequired();
            
            builder.Property(r => r.DateDepart)
                .IsRequired();

            builder.Property(r => r.NombrePersonnes)
                .IsRequired()
                .HasDefaultValue(1);

            builder.Property(r => r.Statut)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(r=> r.RemiseAppliquee)
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0);
            builder.Property(r => r.PenaliteAnnulation)
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0);
            builder.Property(r => r.DateCreation)
                .IsRequired();

            // Définition de la relation Many-to-One entre Reservation et Client
            builder.HasOne(r => r.Client)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
            // realtion One to many entre Reservation et Chambre
            builder.HasOne(r => r.Chambre)
                .WithMany()  //On laisse vide car dans le mini projet ils n'ont pas dit de permettre la consultation des historques de reservations du'une chambres
                .HasForeignKey(r => r.ChambreId)
                .OnDelete(DeleteBehavior.Cascade);

           
        }
    }
}
