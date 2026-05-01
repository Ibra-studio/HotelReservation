using HotelReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Nom { get; set; }
        public required string Couriel { get; set; }

        public required string PasswordHash { get; set; }

        public RoleUser Role { get; set; } = RoleUser.Receptionniste;

        public bool EstActif { get; set; } = true;
    }
}
