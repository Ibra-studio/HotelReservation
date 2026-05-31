using HotelReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Domain.Interfaces
{
    public interface IEquipementRepository
    {
        Task<Equipement?> GetById(Guid id);
        Task<List<Equipement>> GetAll();
        Task Add(Equipement equipement);
        Task Update(Equipement equipement);
        Task Delete(Guid id);
        Task<List<Equipement>> GetByIds(List<Guid> ids);
    }

}
