using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelReservation.Application.Dtos.Client
{
    public record CreateClientDto
    (
       [Required] [StringLength(30)]string Nom,
       [Required][StringLength(50)] string Prenom,
       [Required][StringLength(20)] string NumPieceIdentite,
       [Required][Phone] string NumeroTelephone,
       [EmailAddress] string ? Email,
       [StringLength(100)] string ?  Adresse
     );
}
