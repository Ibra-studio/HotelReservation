using HotelReservation.Application.Dtos.Chambre;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Application.Dtos.Equipement
{
    public record EquipementDto
    (
        Guid Id,
        string Nom

    );
}
