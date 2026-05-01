using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelReservation.Application.Dtos.User
{
    public record LoginDto(
        [Required][EmailAddress] string Courriel,
        [Required] string Password
    );

}
