using HotelReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Domain.Entities
{
    public class Facture
    {
        public Guid Id { get; set; }
        public Guid ReservationId { get; set; }

        public decimal MontantTotal { get; set; }

        public DateTime DateEmission { get; set; } = DateTime.UtcNow;

        public decimal MontantRemise { get; set; }

        public decimal MontantNuitee { get; set; }

        public decimal MontantServices { get; set; }

        public StatutPaiement Statut { get; set; } = StatutPaiement.EnAttente;

        //Navigation
        public Reservation? Reservation { get; set; }
        public List<LigneFacture> LignesFacture { get; set; } = new List<LigneFacture>();


    }
}
