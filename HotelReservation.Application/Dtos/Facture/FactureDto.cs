using HotelReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Application.Dtos.Facture
{
    public record FactureDto
    (
        Guid Id,
        Guid ReservationId,
        DateTime DateEmission,
        decimal MontantTotal,
        decimal MontantNuitee,
        decimal MontantRemise,
        decimal MontantServices,
        StatutPaiement Statut,
        List<LigneFactureDto> LignesFacture
    );
}
