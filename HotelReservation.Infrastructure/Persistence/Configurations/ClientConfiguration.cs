using HotelReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Infrastructure.Persistence.Configurations
{
    public class ClientConfiguration: IEntityTypeConfiguration<Client> //Interface D'ef core qui dit cette classe configure L'entité Client
    {
      public void Configure(EntityTypeBuilder<Client> builder) // c'est l'outil que Ef core te donne pour configurer l'entité Client, il contient toutes les methodes Totable etcc
        {

            //Nom de la table
            builder.ToTable("Clients");

            //Clé primaire
            builder.HasKey(c => c.Id);

            //Propriétés
            builder.Property(c => c.Nom)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Prenom)
                .IsRequired()
                .HasMaxLength (100);
            builder.Property(c => c.NumPieceIdentite)
                .IsRequired()
                .HasMaxLength(50);

            //2 clients ne peuvent pas avoir le même numéro de pièce d'identité
            builder.HasIndex(c=>c.NumPieceIdentite)
                .IsUnique();
            builder.Property(c => c.NumeroTelephone)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(c => c.Email)
                .HasMaxLength(150);
            builder.Property(c => c.Adresse)
                .HasMaxLength(250);
            builder.Property(c => c.EstActif)
                .IsRequired()
                .HasDefaultValue(true);
          


        }
    }
}
