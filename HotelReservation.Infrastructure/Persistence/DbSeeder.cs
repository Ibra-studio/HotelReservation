using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Enums;

namespace HotelReservation.Infrastructure.Persistence
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // Guard — si des données existent déjà, on ne reseed pas
            if (context.Users.Any()) return;

            // ── Users ──
            var admin = new User
            {
                Id = Guid.NewGuid(),
                Nom = "Admin",
                Courriel = "admin@hotel.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                Role = RoleUser.Administrateur,
                EstActif = true
            };

            var receptionniste = new User
            {
                Id = Guid.NewGuid(),
                Nom = "Receptionniste",
                Courriel = "receptionniste@hotel.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Recep123!"),
                Role = RoleUser.Receptionniste,
                EstActif = true
            };

            await context.Users.AddRangeAsync(admin, receptionniste);

            // ── Equipements ──
            var wifi = new Equipement { Id = Guid.NewGuid(), Nom = "WIFI" };
            var tv = new Equipement { Id = Guid.NewGuid(), Nom = "TV" };
            var clim = new Equipement { Id = Guid.NewGuid(), Nom = "Climatisation" };
            var jacuzzi = new Equipement { Id = Guid.NewGuid(), Nom = "Jacuzzi" };
            var balcon = new Equipement { Id = Guid.NewGuid(), Nom = "Balcon" };
            var minibar = new Equipement { Id = Guid.NewGuid(), Nom = "Minibar" };
            var coffre = new Equipement { Id = Guid.NewGuid(), Nom = "Coffre-fort" };
            var telephone = new Equipement { Id = Guid.NewGuid(), Nom = "Téléphone" };
            var frigo = new Equipement { Id = Guid.NewGuid(), Nom = "Réfrigérateur" };

            await context.Equipements.AddRangeAsync(
                wifi, tv, clim, jacuzzi, balcon, minibar, coffre, telephone, frigo
            );

            // ── Chambres ──
            var chambres = new List<Chambre>
            {
                new Chambre
                {
                    Id = Guid.NewGuid(),
                    NumChambre = "101",
                    Type = TypeChambre.Simple,
                    Etage = 1,
                    CapaciteAccueil = 1,
                    Description = "Chambre simple confortable",
                    Statut = StatutChambre.Disponible,
                    Equipements = new List<Equipement> { wifi, tv }
                },
                new Chambre
                {
                    Id = Guid.NewGuid(),
                    NumChambre = "102",
                    Type = TypeChambre.Simple,
                    Etage = 1,
                    CapaciteAccueil = 1,
                    Description = "Chambre simple vue jardin",
                    Statut = StatutChambre.Disponible,
                    Equipements = new List<Equipement> { wifi, telephone }
                },
                new Chambre
                {
                    Id = Guid.NewGuid(),
                    NumChambre = "201",
                    Type = TypeChambre.Double,
                    Etage = 2,
                    CapaciteAccueil = 2,
                    Description = "Chambre double spacieuse",
                    Statut = StatutChambre.Disponible,
                    Equipements = new List<Equipement> { wifi, tv, clim }
                },
                new Chambre
                {
                    Id = Guid.NewGuid(),
                    NumChambre = "202",
                    Type = TypeChambre.Double,
                    Etage = 2,
                    CapaciteAccueil = 2,
                    Description = "Chambre double avec balcon vue mer",
                    Statut = StatutChambre.Disponible,
                    Equipements = new List<Equipement> { wifi, tv, clim, balcon, minibar }
                },
                new Chambre
                {
                    Id = Guid.NewGuid(),
                    NumChambre = "203",
                    Type = TypeChambre.Double,
                    Etage = 2,
                    CapaciteAccueil = 2,
                    Description = "Chambre double avec réfrigérateur",
                    Statut = StatutChambre.Disponible,
                    Equipements = new List<Equipement> { wifi, tv, frigo, coffre }
                },
                new Chambre
                {
                    Id = Guid.NewGuid(),
                    NumChambre = "301",
                    Type = TypeChambre.Suite,
                    Etage = 3,
                    CapaciteAccueil = 4,
                    Description = "Suite luxueuse avec jacuzzi et balcon",
                    Statut = StatutChambre.Disponible,
                    Equipements = new List<Equipement> { wifi, tv, clim, jacuzzi, balcon, minibar, coffre }
                },
                new Chambre
                {
                    Id = Guid.NewGuid(),
                    NumChambre = "302",
                    Type = TypeChambre.Suite,
                    Etage = 3,
                    CapaciteAccueil = 3,
                    Description = "Suite premium avec terrasse panoramique",
                    Statut = StatutChambre.Disponible,
                    Equipements = new List<Equipement> { wifi, tv, clim, balcon, minibar, coffre, frigo }
                },
            };

            await context.Chambres.AddRangeAsync(chambres);

            // ── Tarifs ──
            var tarifs = new List<Tarif>
            {
                new Tarif { Id = Guid.NewGuid(), TypeChambre = TypeChambre.Simple, Saison = Season.BasseSaison, PrixParNuit = 300 },
                new Tarif { Id = Guid.NewGuid(), TypeChambre = TypeChambre.Simple, Saison = Season.HauteSaison, PrixParNuit = 500 },
                new Tarif { Id = Guid.NewGuid(), TypeChambre = TypeChambre.Simple, Saison = Season.PeriodeSpeciale, PrixParNuit = 650 },
                new Tarif { Id = Guid.NewGuid(), TypeChambre = TypeChambre.Double, Saison = Season.BasseSaison, PrixParNuit = 500 },
                new Tarif { Id = Guid.NewGuid(), TypeChambre = TypeChambre.Double, Saison = Season.HauteSaison, PrixParNuit = 800 },
                new Tarif { Id = Guid.NewGuid(), TypeChambre = TypeChambre.Double, Saison = Season.PeriodeSpeciale, PrixParNuit = 950 },
                new Tarif { Id = Guid.NewGuid(), TypeChambre = TypeChambre.Suite, Saison = Season.BasseSaison, PrixParNuit = 900 },
                new Tarif { Id = Guid.NewGuid(), TypeChambre = TypeChambre.Suite, Saison = Season.HauteSaison, PrixParNuit = 1500 },
                new Tarif { Id = Guid.NewGuid(), TypeChambre = TypeChambre.Suite, Saison = Season.PeriodeSpeciale, PrixParNuit = 1800 },
            };

            await context.Tarifs.AddRangeAsync(tarifs);

            await context.SaveChangesAsync();
        }
    }
}