using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Domain.Interfaces
{
    public interface IChambreRepository
    {

        Task<Chambre?> GetById(Guid id);

        Task<List<Chambre>> GetAll();
        Task<List<Chambre>> GetDisponibilite(DateOnly dateArrivee , DateOnly dateDepart);

        Task Add(Chambre chambre);
        Task Update(Chambre chambre);

       
    }
}
