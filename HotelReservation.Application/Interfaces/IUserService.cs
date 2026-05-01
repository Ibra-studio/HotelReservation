using HotelReservation.Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAll();
        Task<UserDto?> GetById(Guid id);
        Task Add(CreateUserDto dto);
        Task Update(Guid id, UpdateUserDto dto);
        Task Desactiver(Guid id);
        Task<string> Login(LoginDto dto);  // ← retourne le token JWT
    }

}
