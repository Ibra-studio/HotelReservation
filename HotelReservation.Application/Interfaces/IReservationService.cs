using HotelReservation.Application.Dtos.Facture;
using HotelReservation.Application.Dtos.Reservation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Application.Interfaces
{
    public interface IReservationService
    {
     
        Task<ReservationDto?> GetById(Guid id);
        Task<List<ReservationDto>> GetByClientId(Guid clientId);
        Task Add(CreateReservationDto dto);
        Task Update(Guid id, UpdateReservationDto dto);
        Task Annuler(Guid id);
        Task CheckIn(Guid id);
        Task<FactureDto?> CheckOut(Guid id);
    }

}
