using HotelReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Application.Dtos.Reservation
{
    public record ReservationDto
    (
        Guid Id,
        Guid ClientId,
        Guid ChambreId,
        DateOnly DateArrivee,
        DateOnly DateDepart,
        int NombrePersonnes,
        DateTime? heureArriveeEffective,
        decimal RemiseAppliquee,
        decimal PenaliteAnnulation,
        StatutReservation Statut,
        DateTime DateCreation

    );
}
