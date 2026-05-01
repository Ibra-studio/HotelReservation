using HotelReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelReservation.Application.Dtos.Reservation
{
    public record CreateReservationDto
    (
       [Required] Guid ClientId,
       [Required] Guid ChambreId,
       [Required][Range(1,10)] int NombrePersonnes,
       [Required] DateOnly DateArrivee,
       [Required] DateOnly DateDepart,
       decimal RemiseAppliquee=0
    );
}
