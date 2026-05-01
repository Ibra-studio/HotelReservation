using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelReservation.Application.Dtos.Client
{
    public record UpdateClientDto(
       [Required][StringLength(30)] string Nom,
       [Required][StringLength(50)] string Prenom,
       [Required][Phone] string NumeroTelephone,
       [Required][EmailAddress] string Email,
       [Required][StringLength(100)] string Adresse
       );
  
}
