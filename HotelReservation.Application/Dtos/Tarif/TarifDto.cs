using HotelReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Application.Dtos.Tarif
{
    public record TarifDto(
        Guid Id,
        TypeChambre TypeChambre,
        Season Saison,
        decimal PrixParNuit
    );

}
