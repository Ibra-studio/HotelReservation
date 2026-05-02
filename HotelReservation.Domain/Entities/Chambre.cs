using HotelReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Domain.Entities
{
    public class Chambre
    {
        public Guid Id { get; set; }

        public  required string NumChambre { get; set; }
        public TypeChambre Type { get; set; } = TypeChambre.Simple;

        public  int Etage { get; set; }

        public  int CapaciteAccueil { get; set; }

        public  string ? Description { get; set; }

        public StatutChambre Statut { get; set; } = StatutChambre.Disponible;


       public List<Equipement> Equipements { get; set; } = new List<Equipement>();
       
    }
}
