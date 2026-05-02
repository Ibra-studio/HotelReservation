using HotelReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Infrastructure.Persistence.Configurations
{
    public class LigneFactureConfiguration: IEntityTypeConfiguration<LigneFacture>
        
    {
        public void Configure(EntityTypeBuilder<LigneFacture> builder) 
        {
            builder.ToTable("LignesFacture");
            builder.HasKey(lf => lf.Id);

            builder.Property(lf=> lf.Description).IsRequired().HasMaxLength(500);


            builder.Property(lf=>lf.Quantite).IsRequired();

            builder.Property(lf=>lf.Montant).IsRequired().HasColumnType("decimal(10,2)");

            builder.Property(lf => lf.PrixUnitaire).IsRequired().HasColumnType("decimal(10,2)");


            //Relation one to many avec facture

            builder.HasOne(lf => lf.Facture)
                   .WithMany(f => f.LignesFacture)
                   .HasForeignKey(lf => lf.FactureId)
                   .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
