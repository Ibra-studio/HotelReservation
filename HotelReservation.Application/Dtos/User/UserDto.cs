using HotelReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Application.Dtos.User
{
    public record UserDto
    (
        Guid Id,
        string Nom,
        string Courriel,
        RoleUser Role,
        bool EstActif
    );
}
