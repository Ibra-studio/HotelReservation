using HotelReservation.Application.Dtos.Facture;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Application.Interfaces
{
    public interface IFactureService
    {
        Task<FactureDto?> GetById(Guid id);
        Task<FactureDto?> GetByReservationId(Guid reservationId);
        Task<List<FactureDto>> GetByClientId(Guid clientId);
        Task<FactureDto?> Generer(Guid reservationId);
    }

}
