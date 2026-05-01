using HotelReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Domain.Entities
{
    public class Tarif
    {
        public Guid Id { get; set; }
        
        public TypeChambre TypeChambre { get; set; } = TypeChambre.Simple;

        public Season Saison { get; set; } = Season.BasseSaison;

        public decimal PrixParNuit { get; set; }

        public DateOnly DateDebut { get; set; } // date de début de validité du tarif pour la haute saison par exemple 1 juillet

        public DateOnly DateFin { get; set; } // date de fin de validité du tarif pour la haute saison par exemple 31 août
       

    }
}
