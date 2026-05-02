using HotelReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelReservation.Application.Dtos.User
{
    public record UpdateUserDto
     (
         string Nom,
         string Courriel,
         RoleUser Role
      );
}
