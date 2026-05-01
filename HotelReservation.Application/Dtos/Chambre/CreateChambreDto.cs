using HotelReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelReservation.Application.Dtos.Chambre
{
    public record CreateChambreDto
 (
     [Required] string numChambre,
     TypeChambre Type,
     [Range(1,100)]int Etage,
     [Range(1,10)]int CapaciteAccueil,
     string? Description,
     List<Guid> EquipementIds,
     StatutChambre Statut = StatutChambre.Disponible
  );
}

