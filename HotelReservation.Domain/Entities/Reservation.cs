using HotelReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Domain.Entities
{
    public class Reservation
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public Guid ChambreId { get; set; }


        public DateOnly DateArrivee { get; set; }
        public DateOnly DateDepart { get; set; }

      
        public int NombrePersonnes { get; set; } = 1;

        public StatutReservation Statut { get; set; } = StatutReservation.Confirmee;
        
        public DateTime HeureArriveeEffective { get; set; }

        public decimal RemiseAppliquee { get; set; } = 0;

        public decimal PenaliteAnnulation { get; set; } = 0;

        public DateTime DateCreation { get; set; } = DateTime.UtcNow;

        //Navigation 

        public Client? Client { get; set; }
        public Chambre? Chambre { get; set; }

        public Facture? Facture { get; set; }



    }
}
