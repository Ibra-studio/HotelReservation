using HotelReservation.Application.Dtos.Equipement;
using HotelReservation.Application.Interfaces;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Application.Services
{
    public class EquipementService : IEquipementService
    {
        private readonly IEquipementRepository _equipementRepository;

        public EquipementService(IEquipementRepository equipementRepository)
        {
            _equipementRepository = equipementRepository;
        }

        public async Task<List<EquipementDto>> GetAll()
        {
            var equipements = await _equipementRepository.GetAll();
            return equipements.Select(e => new EquipementDto(
                e.Id,
                e.Nom
            )).ToList();
        }

        public async Task<EquipementDto?> GetById(Guid id)
        {
            var equipement = await _equipementRepository.GetById(id);
            if (equipement == null) return null;
            return new EquipementDto(equipement.Id, equipement.Nom);
        }

        public async Task Add(CreateEquipementDto dto)
        {
            var equipement = new Equipement
            {
                Id = Guid.NewGuid(),
                Nom = dto.Nom
            };
            await _equipementRepository.Add(equipement);
        }

        public async Task Update(Guid id, UpdateEquipementDto dto)
        {
            var equipement = await _equipementRepository.GetById(id);
            if (equipement == null)
                throw new Exception("Equipement introuvable");
            equipement.Nom = dto.Nom;
            await _equipementRepository.Update(equipement);
        }

        public async Task Delete(Guid id)
        {
            await _equipementRepository.Delete(id);
        }
    }

}
