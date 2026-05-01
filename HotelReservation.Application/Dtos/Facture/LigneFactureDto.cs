using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Application.Dtos.Facture
{
    public record LigneFactureDto
    (
        Guid Id,
        string Description,
        decimal Montant,
        int Quantite,
        decimal PrixUnitaire
    );
}
