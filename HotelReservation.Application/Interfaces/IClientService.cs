using HotelReservation.Application.Dtos.Client;
using HotelReservation.Application.Dtos.Reservation;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Application.Interfaces
{
    public interface IClientService
    {
        Task<List<ClientDto>> GetAll();
        Task<ClientDto?> GetById(Guid id);
        Task<ClientDto?> GetByNom(string nom);
        Task<ClientDto?> GetByNumIdentite(string num);
   
        Task<List<ReservationDto>> GetHistorique(Guid clientId);
        Task Add(CreateClientDto dto);
        Task Update(Guid id, UpdateClientDto dto);
        Task Desactiver(Guid id);
    }

}
