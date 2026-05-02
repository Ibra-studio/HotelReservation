using HotelReservation.Application.Dtos.Chambre;
using HotelReservation.Application.Dtos.Equipement;
using HotelReservation.Application.Interfaces;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Enums;
using HotelReservation.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Application.Services
{
    public class ChambreService : IChambreService
    {
        private readonly IChambreRepository _chambreRepository;
        private readonly IEquipementRepository _equipementRepository;



        public ChambreService(IChambreRepository chambreRepository, IEquipementRepository equipementRepository)
        {
            _chambreRepository = chambreRepository;
            _equipementRepository = equipementRepository;
        }

        //Avec ça tu peux afficher une vue complete des chambres avec surtout leur statut si occupee ou libre ou en maintenance
        public async Task<List<ChambreDto>> GetAll()
        {
            var chambres = await _chambreRepository.GetAll();

            return chambres.Select(c => new ChambreDto
            (
               c.Id,
               c.NumChambre,
               c.Type,
               c.Etage,
               c.CapaciteAccueil,
               c.Description,
               c.Statut,
               c.Equipements.Select(e => new EquipementDto
               (
                   e.Id,
                   e.Nom
               )).ToList()

            )).ToList();
        }
        public async Task<ChambreDto?> GetById(Guid id)
        {
            var chambre = await _chambreRepository.GetById(id);
            if (chambre == null) return null;
            return new ChambreDto
            (
               chambre.Id,
               chambre.NumChambre,
               chambre.Type,
               chambre.Etage,
               chambre.CapaciteAccueil,
               chambre.Description,
               chambre.Statut,

               chambre.Equipements.Select(e => new EquipementDto
               (
                   e.Id,
                   e.Nom
               )).ToList()
            );

        }
        public async Task<List<ChambreDto>> GetDisponibles(DateOnly dateArrivee, DateOnly dateDepart)
        {
            var chambres = await _chambreRepository.GetDisponibilite(dateArrivee, dateDepart);
            return chambres.Select(c => new ChambreDto
            (
               c.Id,
               c.NumChambre,
               c.Type,
               c.Etage,
               c.CapaciteAccueil,
               c.Description,
               c.Statut,
               c.Equipements.Select(e => new EquipementDto
               (
                   e.Id,
                   e.Nom
                )).ToList()
            )).ToList();
        }

        public async Task Add(CreateChambreDto dto)
        {
            var chambre = new Chambre
            {
                Id = Guid.NewGuid(),
                NumChambre = dto.NumChambre,
                Type = dto.Type,
                Etage = dto.Etage,
                CapaciteAccueil = dto.CapaciteAccueil,
                Description = dto.Description
            };
            await _chambreRepository.Add(chambre);
        }
        public async Task Update(Guid id, UpdateChambreDto dto)
        {
            var chambre = await _chambreRepository.GetById(id);
            if (chambre == null) throw new Exception("Chambre introuvable");
            chambre.NumChambre = dto.NumChambre;
            chambre.Type = dto.Type;
            chambre.CapaciteAccueil = dto.CapaciteAccueil;
            chambre.Description = dto.Description;
            chambre.Statut = dto.Statut;
            await _chambreRepository.Update(chambre);

        }
        public async Task Desactiver(Guid id)
        {
            var chambre = await _chambreRepository.GetById(id);
            if (chambre == null) throw new Exception("Chambre introuvable");
            chambre.Statut = StatutChambre.Desactivee;
            await _chambreRepository.Update(chambre);
        }

        public async Task AjouterEquiments(Guid chambreId, Guid equipementId)
        {
            var chambre = await _chambreRepository.GetById(chambreId);
            if (chambre == null) throw new Exception("Chambre Introuvable");
            var equipement = await _equipementRepository.GetById(equipementId);
            if (equipement == null) throw new Exception("Equipement introuvable");

            bool DejaAjoute = chambre.Equipements.Any(e => e.Id == equipementId);
            if (DejaAjoute) throw new Exception("Equipement déjà associé à cette chambre");

            chambre.Equipements.Add(equipement);
            await _chambreRepository.Update(chambre);
        }
        public async Task RetirerEquipements(Guid chambreId, Guid equipementId)
        {
            var chambre = await _chambreRepository.GetById(chambreId);
            if (chambre == null) throw new Exception("Chambre Introuvable");
            var equipement = await _equipementRepository.GetById(equipementId);
            if (equipement == null) throw new Exception("Equipement introuvable");
            bool Existe = chambre.Equipements.Any(e => e.Id == equipementId);
            if (!Existe) throw new Exception("Equipement non associé à cette chambre");
            chambre.Equipements.Remove(equipement);
            await _chambreRepository.Update(chambre);
        }
    }
}
