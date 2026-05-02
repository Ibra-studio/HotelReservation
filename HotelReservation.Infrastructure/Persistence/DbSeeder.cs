using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Infrastructure.Persistence
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // Vérifier si un admin existe déjà
            if (!context.Users.Any(u => u.Role == RoleUser.Administrateur))
            {
                var admin = new User
                {
                    Id = Guid.NewGuid(),
                    Nom = "Admin",
                    Courriel = "admin@hotel.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    Role = RoleUser.Administrateur,
                    EstActif = true
                };
                await context.Users.AddAsync(admin);
                await context.SaveChangesAsync();
            }
        }
    }

}
