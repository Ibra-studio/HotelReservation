using HotelReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Domain.Interfaces
{
    public interface IClientRepository
    {

        Task<Client?> GetById(Guid id);
        Task<List<Client>> GetAll();

        Task<Client?> GetByNom (string nom);
        Task<Client?> GetByNumIdentite(string num);
        Task Add(Client client);
        Task Update(Client client);
      
    }
}
