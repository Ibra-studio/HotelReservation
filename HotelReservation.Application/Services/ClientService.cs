using HotelReservation.Application.Dtos.Client;
using HotelReservation.Application.Dtos.Facture;
using HotelReservation.Application.Dtos.Reservation;
using HotelReservation.Application.Interfaces;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Application.Services
{
    public class ClientService : IClientService
    {

        private readonly IClientRepository _clientRepository;
        private readonly IReservationRepository _reservationRepository;
       

        public ClientService(IClientRepository clientRepository, IReservationRepository reservationRepository,IFactureRepository factureRepository)
        {
            _clientRepository = clientRepository;
            _reservationRepository = reservationRepository;
         
        }

        public async Task<List<ClientDto>> GetAll()
        {
            var clients = await _clientRepository.GetAll();
            return clients.Select(c => new ClientDto
            (
                c.Id,
                c.Nom,
                c.Prenom,
                c.NumPieceIdentite,
                c.NumeroTelephone,
                c.Email,
                c.Adresse,
                c.EstActif
            )).ToList();
        }
        public async Task<ClientDto?> GetById(Guid id)
        {
            var client = await _clientRepository.GetById(id);
            if (client == null) return null;
            return new ClientDto
            (
                client.Id,
                client.Nom,
                client.Prenom,
                client.NumPieceIdentite,
                client.NumeroTelephone,
                client.Email,
                client.Adresse,
                client.EstActif
            );

        }

        public async Task<ClientDto?> GetByNom(string nom)
        {
            var client = await _clientRepository.GetByNom(nom);
            if (client == null) return null;
            return new ClientDto
            (
                client.Id,
                client.Nom,
                client.Prenom,
                client.NumPieceIdentite,
                client.NumeroTelephone,
                client.Email,
                client.Adresse,
                client.EstActif
            );
        }

        public async Task<ClientDto?> GetByNumIdentite(string num)

        {
            var client = await _clientRepository.GetByNumIdentite(num);
            if (client == null) return null;
            return new ClientDto
            (
                client.Id,
                client.Nom,
                client.Prenom,
                client.NumPieceIdentite,
                client.NumeroTelephone,
                client.Email,
                client.Adresse,
                client.EstActif
            );
        }
        public async Task<List<ReservationDto>> GetHistorique(Guid clientId)
        {
            var reservations = await _reservationRepository.GetByClientId(clientId);
            return reservations.Select(r => new ReservationDto
            (
                r.Id,
                r.ClientId,
                r.ChambreId,
                r.DateArrivee,
                r.DateDepart,
                r.NombrePersonnes,
                r.HeureArriveeEffective,
                r.RemiseAppliquee,
                r.PenaliteAnnulation,
                r.Statut,
                r.DateCreation,
                r.Facture == null ? null : new FactureDto
                (
                   r.Facture.Id,
                   r.Facture.ReservationId,
                   r.Facture.DateEmission,
                   r.Facture.MontantTotal,
                   r.Facture.MontantNuitee,
                   r.Facture.MontantPenalitee,
                   r.Facture.MontantRemise,
                   r.Facture.MontantServices,
                   r.Facture.Statut,
                   r.Facture.LignesFacture.Select(l => new LigneFactureDto
                   (
                       l.Id,
                       l.Description,
                       l.Montant,
                       l.Quantite,
                       l.PrixUnitaire
                   )).ToList()
                )
            )).ToList();
        }
        public async Task Add(CreateClientDto dto)
        {
            var client = new Client
            {
                Id = Guid.NewGuid(),
                Nom = dto.Nom,
                Prenom = dto.Prenom,
                NumPieceIdentite = dto.NumPieceIdentite,
                NumeroTelephone = dto.NumeroTelephone,
                Email = dto.Email,
                Adresse = dto.Adresse

            };
            await _clientRepository.Add(client);
        }
        public async Task Update(Guid id, UpdateClientDto dto)
        {
            var client = await _clientRepository.GetById(id);
            if (client == null) throw new KeyNotFoundException("Client introuvable");
            client.Nom = dto.Nom;
            client.Prenom = dto.Prenom;
            client.NumeroTelephone = dto.NumeroTelephone;
            client.Email = dto.Email;
            client.Adresse = dto.Adresse;
            await _clientRepository.Update(client);
        }
        public async Task Desactiver(Guid id)
        {
            var client = await _clientRepository.GetById(id);
            if (client == null) throw new KeyNotFoundException("Client introuvable");
            client.EstActif = false;
            await _clientRepository.Update(client);
        }
    }
}
