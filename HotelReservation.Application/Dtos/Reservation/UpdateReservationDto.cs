using HotelReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelReservation.Application.Dtos.Reservation
{
    public record UpdateReservationDto
    (
       [Required] Guid ChambreId,
       [Range(1, 10)] int NombrePersonnes,
       [Required] DateOnly DateArrivee,
       [Required] DateOnly DateDepart,
       [Required] decimal RemiseAppliquee
    );
}
