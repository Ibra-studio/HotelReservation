using HotelReservation.Application.Dtos.Equipement;
using HotelReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Application.Dtos.Chambre
{
    public  record ChambreDto
    (
        Guid Id,
        string numChambre,
        TypeChambre Type,
        int Etage,
        int CapaciteAccueil,
        string? Description,
        StatutChambre Statut,
        List<EquipementDto> Equipements
     );
}
