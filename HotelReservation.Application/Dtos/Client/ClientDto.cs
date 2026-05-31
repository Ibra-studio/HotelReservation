using HotelReservation.Application.Dtos.Equipement;
using HotelReservation.Application.Dtos.Reservation;
using HotelReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Application.Dtos.Client
{
    public record ClientDto
    (
       Guid Id,
       string Nom,
       string Prenom,
       string NumPieceIdentite,
       string NumeroTelephone,
       string Email,
       string Adresse,
       bool EstActif,
       List<IdReservationDto> Reservations
    );
}
