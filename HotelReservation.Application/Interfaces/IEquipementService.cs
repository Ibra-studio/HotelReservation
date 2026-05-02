using HotelReservation.Application.Dtos.Equipement;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Application.Interfaces
{
    public interface IEquipementService
    {
        Task<List<EquipementDto>> GetAll();
        Task<EquipementDto?> GetById(Guid id);
        Task Add(CreateEquipementDto dto);
        Task Update(Guid id, UpdateEquipementDto dto);
        Task Delete(Guid id);
    }

}
