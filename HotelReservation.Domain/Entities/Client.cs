using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Domain.Entities
{
    public class Client
    {
        public Guid Id { get; set; }
        public required string  Nom { get; set; }
        public required string Prenom { get; set; }
        public required string NumPieceIdentite { get; set; }
        public required string NumeroTelephone { get; set; }
        public string ? Email { get; set; }
        public  string ?  Adresse { get; set;}

        public bool EstActif { get; set; } = true;

        public List<Reservation> Reservations { get; set; } = new List<Reservation>();


    }
}
