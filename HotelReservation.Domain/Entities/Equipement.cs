using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Domain.Entities
{
    public class Equipement
    {
        public Guid Id { get; set; }

        public required string Nom { get; set; }

        public List<Chambre> Chambres { get; set; } = new List<Chambre>();


    }
}
