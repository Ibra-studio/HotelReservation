using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Domain.Interfaces
{
    public interface ITarifRepository
    {
        Task<Tarif?> GetById(Guid id);
        Task<Tarif?> GetByTypeAndSaison(TypeChambre typeChambre, Season saison);
        Task<List<Tarif>> GetAll();

        Task Add(Tarif tarif);
        Task Update(Tarif tarif);

        Task Delete(Tarif tarif);

    }
}
