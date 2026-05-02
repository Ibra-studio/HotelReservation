using HotelReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Domain.Interfaces
{
    public interface IFactureRepository
    {

      Task<Facture?> GetById(Guid id);

      Task<Facture?> GetByReservationId(Guid reservationId);

      Task<List<Facture>> GetByClientId(Guid clientId);

      Task<List<Facture>> GetAll();

      Task Add(Facture facture);

 
    }
}
