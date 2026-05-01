using HotelReservation.Application.Dtos.Chambre;
using HotelReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Application.Interfaces
{
    public interface IChambreService
    {
        Task<List<ChambreDto>> GetAll();
        Task<ChambreDto?> GetById(Guid id);
        Task<List<ChambreDto>> GetDisponibles(DateOnly dateArrivee, DateOnly dateDepart);


        Task Add(CreateChambreDto dto);
        Task Update(Guid id, UpdateChambreDto dto);
        Task Delete(Guid id);
        Task AjouterEquiments(Guid chambreId, Guid equipementId);
        Task RetirerEquiments(Guid chambreId, Guid equipementId);
    }

}
