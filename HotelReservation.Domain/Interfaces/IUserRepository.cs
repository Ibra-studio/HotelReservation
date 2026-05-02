using HotelReservation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Domain.Interfaces
{
    public interface IUserRepository
    {

            Task<User?> GetById(Guid id);
          
            Task<User?> GetByCourriel(string courriel);
            Task<List<User>> GetAll();
    
            Task Add(User user);
            Task Update(User user);
           
    }
}
