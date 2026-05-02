using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelReservation.Application.Dtos.User
{
    public record UpdatePasswordDto
   (

      [StringLength(100, MinimumLength = 6)] string AncienMotDePasse,
       [StringLength(100, MinimumLength = 6)] string NouveauMotDePasse,
       [StringLength(100, MinimumLength = 6)] string ConfirmationMotDePasse
   );
}
