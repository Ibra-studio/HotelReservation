using HotelReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelReservation.Application.Dtos.User
{
    public record UpdateUserDto
     (
         [Required] string Nom,
         [Required] [EmailAddress] string Courriel,
         RoleUser Role
       );
}
