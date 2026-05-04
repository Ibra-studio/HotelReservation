using HotelReservation.Application.Dtos.Tarif;
using HotelReservation.Application.Interfaces;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Enums;
using HotelReservation.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Application.Services
{
    public class TarifService: ITarifService
    {
        private readonly ITarifRepository _tarifRepository;

        public TarifService(ITarifRepository tarifRepository)
        {
            _tarifRepository = tarifRepository;
        }
        
        public async Task<TarifDto?> GetById(Guid id)
        {
            var tarif = await _tarifRepository.GetById(id);
            if (tarif == null)
                return null;
            return new TarifDto
            (
                tarif.Id,
                tarif.TypeChambre,
                tarif.Saison,
                tarif.PrixParNuit
            );
        }
        public async Task<List<TarifDto>> GetAll()
        {
            var tarifs = await _tarifRepository.GetAll();
            return tarifs.Select(t => new TarifDto
            (
                t.Id,
                t.TypeChambre,
                t.Saison,
                t.PrixParNuit
            )).ToList();
        }
        public async Task<TarifDto?> GetByTypeAndSaison(TypeChambre type, Season saison)
        {
            var tarif = await _tarifRepository.GetByTypeAndSaison(type, saison);
            if (tarif == null)
                return null;
            return new TarifDto
            (
                tarif.Id,
                tarif.TypeChambre,
                tarif.Saison,
                tarif.PrixParNuit
            );
        }

        public async Task Add(CreateTarifDto dto)
        {

            var TarifExist = await _tarifRepository.GetByTypeAndSaison(dto.TypeChambre, dto.Saison);
            if (TarifExist !=null) throw new InvalidOperationException("Un tarif existe déjà pour ce type de chambre et cette saison");
            var tarif = new Tarif
             {
                 Id= Guid.NewGuid(),
                 TypeChambre = dto.TypeChambre,
                 Saison = dto.Saison,
                 PrixParNuit=dto.PrixParNuit

            };
            await _tarifRepository.Add(tarif);
        }

        public async Task Update(Guid id, UpdateTarifDto dto)
        {
            var tarif = await _tarifRepository.GetById(id);
            if (tarif == null)
                return;

            tarif.TypeChambre = dto.TypeChambre;
            tarif.Saison = dto.Saison;
            tarif.PrixParNuit = dto.PrixParNuit;

            await _tarifRepository.Update(tarif);
        }





    }
}
