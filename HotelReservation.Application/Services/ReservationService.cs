using HotelReservation.Application.Dtos.Facture;
using HotelReservation.Application.Dtos.Reservation;
using HotelReservation.Application.Interfaces;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Enums;
using HotelReservation.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Application.Services
{
    public class ReservationService: IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IFactureService _factureService;
        private readonly IChambreRepository _chambreRepository;


        public ReservationService(IReservationRepository reservationRepository, IFactureService factureService, IChambreRepository chambreRepository)
        {
            _reservationRepository = reservationRepository;
            _factureService = factureService;
            _chambreRepository = chambreRepository;
            
        }

        public async Task<ReservationDto?> GetById(Guid id)
        {
            var reservation = await _reservationRepository.GetById(id);
            if (reservation == null) return null;
            return new ReservationDto
                (
                  reservation.Id,
                  reservation.ClientId,
                  reservation.ChambreId,
                  reservation.DateArrivee,
                  reservation.DateDepart,
                  reservation.NombrePersonnes,
                  reservation.HeureArriveeEffective,
                  reservation.RemiseAppliquee,
                  reservation.PenaliteAnnulation,
                  reservation.Statut,
                  reservation.DateCreation
                );

        }
        public async Task<List<ReservationDto>> GetByClientId(Guid clientId)
        {
            var reservations = await _reservationRepository.GetByClientId(clientId);
            return reservations.Select(reservation => new ReservationDto
                (
                  reservation.Id,
                  reservation.ClientId,
                  reservation.ChambreId,
                  reservation.DateArrivee,
                  reservation.DateDepart,
                  reservation.NombrePersonnes,
                  reservation.HeureArriveeEffective,
                  reservation.RemiseAppliquee,
                  reservation.PenaliteAnnulation,
                  reservation.Statut,
                  reservation.DateCreation
                )).ToList();
        }
        public async Task Add(CreateReservationDto dto)
        {
            // on Cherche toute les chambres disponibles pour les dates demandées
            var ChambresDisponible = await _chambreRepository.GetDisponibilite(dto.DateArrivee, dto.DateDepart);

            //ensuite on vérifie que la chambre demandée est bien dans la liste des chambres disponibles
            bool estDisponible = ChambresDisponible.Any(c => c.Id == dto.ChambreId);
            if (!estDisponible) throw new Exception("Chambre non disponible pour les dates sélectionnées");
            var reservation = new Reservation
            {
                Id= Guid.NewGuid(),
                ClientId = dto.ClientId,
                ChambreId = dto.ChambreId,
                DateArrivee = dto.DateArrivee,
                DateDepart = dto.DateDepart,
                NombrePersonnes = dto.NombrePersonnes,
                RemiseAppliquee = dto.RemiseAppliquee,
                Statut = StatutReservation.Confirmee,
                DateCreation = DateTime.UtcNow
            };
            await _reservationRepository.Add(reservation);
        }

        public async Task Update(Guid id, UpdateReservationDto dto)
        {
            var reservation = await _reservationRepository.GetById(id);
            if (reservation == null) throw new Exception("Reservation introuvable");

            if (reservation.Statut == StatutReservation.CheckInEffectue)
                throw new Exception("Impossible de modifier : la reservation est déjà en cours");

            if (reservation.Statut == StatutReservation.CheckOutEffectue)
                throw new Exception("Impossible de modifier : le séjour est terminé");

            if (reservation.Statut == StatutReservation.Annulee)
                throw new Exception("Impossible de modifier : la réservation est annulée");


            // on Cherche toute les chambres disponibles pour les dates demandées
            var ChambresDisponible = await _chambreRepository.GetDisponibilite(dto.DateArrivee, dto.DateDepart);

            //ensuite on vérifie que la chambre demandée est bien dans la liste des chambres disponibles
            bool estDisponible = ChambresDisponible.Any(c => c.Id == dto.ChambreId);
            if (!estDisponible) throw new Exception("Chambre non disponible pour les dates sélectionnées");


            reservation.ChambreId = dto.ChambreId;
            reservation.DateArrivee = dto.DateArrivee;
            reservation.DateDepart = dto.DateDepart;
            reservation.NombrePersonnes = dto.NombrePersonnes;
            reservation.RemiseAppliquee = dto.RemiseAppliquee;
            await _reservationRepository.Update(reservation);
        }

        public async Task Annuler(Guid id)
        {
            var reservation = await _reservationRepository.GetById(id);
            if (reservation == null) throw new Exception("Reservation introuvable");

            if(reservation.Statut == StatutReservation.CheckOutEffectue) throw new Exception("On ne peut pas annuler une réservation déjà terminée");
            if(reservation.Statut == StatutReservation.Annulee) throw new Exception("La réservation est déjà annulée");

            reservation.Statut = StatutReservation.Annulee;
            reservation.PenaliteAnnulation = CalculerPenalite(reservation);
            await _reservationRepository.Update(reservation);
        }

        public async Task CheckIn(Guid id)
        {
            var reservation = await _reservationRepository.GetById(id);
            if (reservation == null) throw new Exception("Reservation introuvable");
            reservation.Statut = StatutReservation.CheckInEffectue;
            reservation.HeureArriveeEffective = DateTime.UtcNow;
            await _reservationRepository.Update(reservation);
        }

        public async Task<FactureDto> CheckOut(Guid reservationId)
        {
            var reservation = await _reservationRepository.GetById(reservationId);
            if (reservation == null) throw new Exception("Reservation introuvable");
            if(reservation.Statut != StatutReservation.CheckInEffectue) throw new Exception("Check-in non effectué, impossible de faire le check-out");
            reservation.Statut = StatutReservation.CheckOutEffectue;
            await _reservationRepository.Update(reservation);
            return await _factureService.Generer(reservationId);
        }


        private decimal CalculerPenalite(Reservation reservation)
        {
            var joursAvantArrivee = (reservation.DateArrivee.ToDateTime(TimeOnly.MinValue) - DateTime.UtcNow).TotalDays;
            if (joursAvantArrivee >= 7) return 0; // Pas de pénalité si l'annulation est faite au moins 7 jours avant l'arrivée
            if (joursAvantArrivee >= 3) return reservation.NombrePersonnes * 50; // Pénalité de 50€ par personne si l'annulation est faite entre 3 et 7 jours avant l'arrivée
            return reservation.NombrePersonnes * 100; // Pénalité de 100€ par personne si l'annulation est faite moins de 3 jours avant l'arrivée
        }


    }
}
