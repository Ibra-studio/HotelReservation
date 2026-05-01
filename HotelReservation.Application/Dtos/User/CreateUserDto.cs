using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelReservation.Application.Dtos.User
{
    public record CreateUserDto
     (
         [Required] string Nom,
         [Required] string Couriel,
         [Required] string Role,
        [Required][StringLength(100, MinimumLength = 6)] string Password
     );
}
