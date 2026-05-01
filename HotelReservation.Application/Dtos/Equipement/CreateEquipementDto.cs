using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelReservation.Application.Dtos.Equipement
{
    public record CreateEquipementDto
    (
        [Required][StringLength(50)] string Nomr
    );
}
