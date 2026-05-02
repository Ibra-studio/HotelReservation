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

        private int _capaciteAccueil;
        public  int CapaciteAccueil
        {
            get => _capaciteAccueil;
            set
            {
                if (value < 1 || value > 5)
                    throw new ArgumentOutOfRangeException(nameof(CapaciteAccueil), "La capacité d'accueil doit être comprise entre 1 et 5.");
                _capaciteAccueil = value;
            }
        }

        public  string ? Description { get; set; }

        public StatutChambre Statut { get; set; } = StatutChambre.Disponible;


       public List<Equipement> Equipements { get; set; } = new List<Equipement>();
       
    }
}
