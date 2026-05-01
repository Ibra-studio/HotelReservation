using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Domain.Entities
{
    public class LigneFacture
    {
        public Guid Id { get; set; }

        public Guid FactureId { get; set; }

        public required string Description { get; set; }

        public int Quantite { get; set; }


        public decimal Montant { get; set; }
        public decimal PrixUnitaire { get; set; }

        //Navigation
        public Facture? Facture { get; set; }
    }
}
