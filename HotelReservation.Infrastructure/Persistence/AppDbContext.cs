using HotelReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Infrastructure.Persistence
{

    //DbContextOptions contient la connection string injectée depuis appsettings.json
    public class AppDbContext (DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Client> Clients {get; set; }
        public DbSet<Chambre> Chambres { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Facture> Factures { get; set; }
        public DbSet<LigneFacture> LignesFacture { get; set; }
        public DbSet<Tarif> Tarifs { get; set; }
        public DbSet<Equipement> Equipements { get; set; }
        
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Trouve et applique automatiquement toutes les XXXConfigurations.cs
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        
    }
}
