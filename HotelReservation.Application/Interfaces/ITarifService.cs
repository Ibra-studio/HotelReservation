using HotelReservation.Application.Dtos.Tarif;
using HotelReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Application.Interfaces
{
    public interface ITarifService
    {

       Task<TarifDto?> GetById(Guid id);
        Task<List<TarifDto>> GetAll();
        Task<TarifDto?> GetByTypeAndSaison(TypeChambre type, Season saison);
        Task Add(CreateTarifDto dto);
        Task Update(Guid id,UpdateTarifDto dto);
    }

}
