using HotelReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Domain.Interfaces
{
    public interface IReservationRepository
    {
        Task<Reservation?> GetById(Guid id);
        Task<List<Reservation>> GetByClientId(Guid clientId);

        Task Add(Reservation reservation);
        Task Update(Reservation reservation);
        Task Delete(Guid id);

    }
}
