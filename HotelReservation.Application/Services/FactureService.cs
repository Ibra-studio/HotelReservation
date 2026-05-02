using HotelReservation.Application.Dtos.Facture;
using HotelReservation.Application.Interfaces;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Enums;
using HotelReservation.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace HotelReservation.Application.Services
{
    public class FactureService : IFactureService
    {
        private readonly IFactureRepository _factureRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IChambreRepository _chambreRepository;
        private readonly ITarifRepository _tarifRepository;


        public FactureService(IFactureRepository factureRepository, IReservationRepository reservationRepository, IChambreRepository chambreRepository,ITarifRepository tarifRepository)
        {
            _factureRepository = factureRepository;
            _reservationRepository = reservationRepository;
            _chambreRepository = chambreRepository;
            _tarifRepository = tarifRepository;
            
        }

        public async Task<FactureDto?> GetById(Guid id)
        {
            var facture = await _factureRepository.GetById(id);
            if (facture == null)
                return null;

            return new FactureDto
            (
                facture.Id,
                facture.ReservationId,
                facture.DateEmission,
                facture.MontantTotal,
                facture.MontantNuitee,
                facture.MontantRemise,
                facture.MontantServices,
                facture.Statut,
                facture.LignesFacture.Select(l => new LigneFactureDto
                (
                    l.Id,
                    l.Description,
                    l.Montant,
                    l.Quantite,
                    l.PrixUnitaire
                )).ToList()
            );
        }
        public async Task<FactureDto?> GetByReservationId(Guid reservationId)
        {
            var facture = await _factureRepository.GetByReservationId(reservationId);
            if (facture == null)
                return null;
            return new FactureDto
            (
                facture.Id,
                facture.ReservationId,
                facture.DateEmission,
                facture.MontantTotal,
                facture.MontantNuitee,
                facture.MontantRemise,
                facture.MontantServices,
                facture.Statut,
                facture.LignesFacture.Select(l => new LigneFactureDto
                (
                    l.Id,
                    l.Description,
                    l.Montant,
                    l.Quantite,
                    l.PrixUnitaire
                )).ToList()
            );
        }

        public async Task<List<FactureDto>> GetByClientId(Guid clientId)
        {
            var factures = await _factureRepository.GetByClientId(clientId);
            return factures.Select(f => new FactureDto
            (
                f.Id,
                f.ReservationId,
                f.DateEmission,
                f.MontantTotal,
                f.MontantNuitee,
                f.MontantRemise,
                f.MontantServices,
                f.Statut,
                f.LignesFacture.Select(l => new LigneFactureDto
                (
                    l.Id,
                    l.Description,
                    l.Montant,
                    l.Quantite,
                    l.PrixUnitaire
                )).ToList()
            )).ToList();
        }

        public async Task<FactureDto?> Generer(Guid reservationId)
        {
            var reservation = await _reservationRepository.GetById(reservationId);
            if (reservation == null)
                return null;
            if(reservation.Statut != StatutReservation.CheckInEffectue)
                throw new InvalidOperationException("La facture ne peut être générée que pour une réservation avec le statut 'CheckInEffectue'.");

            var chambre = await _chambreRepository.GetById(reservation.ChambreId);
            if (chambre == null) throw new Exception("Chambre introuvable");

            var saison = DeterminerSaison(reservation.DateArrivee);
            var tarif= await _tarifRepository.GetByTypeAndSaison(chambre.Type, saison);
            if (tarif == null) throw new Exception("Tarif introuvable pour le type de chambre et la saison donnée.");

            //calculer montant nuitee DayNumber au lieu de Day pour la difference entre 2 dates
            var nombreNuitees = (reservation.DateDepart.DayNumber - reservation.DateArrivee.DayNumber);
            //Calcul des Montants
            var montantNuitee = nombreNuitees * tarif.PrixParNuit;
            var montantRemise = reservation.RemiseAppliquee;
            var montantTotal = montantNuitee - montantRemise;

            var lignes=new List<LigneFacture>();

            //Ligne Nuitee
            var lignesNuitee = new LigneFacture
            {
                Id = Guid.NewGuid(),
                Description = $"{nombreNuitees} nuit(s) - Chambre {chambre.NumChambre}",
                Montant = montantNuitee,
                Quantite = nombreNuitees,
                PrixUnitaire = tarif.PrixParNuit
            };
            lignes.Add(lignesNuitee);
            //Ligne Remise si applicable

            if(reservation.RemiseAppliquee >0)
            {
                
                var ligneRemise = new LigneFacture
                {
                    Id = Guid.NewGuid(),
                    Description = "Remise commerciale",
                    Montant = -reservation.RemiseAppliquee,
                    Quantite = 1,
                    PrixUnitaire = -reservation.RemiseAppliquee
                };
                lignes.Add(ligneRemise);
            }

            var facture= new Facture
            {
                Id = Guid.NewGuid(),
                ReservationId = reservationId,
                DateEmission = DateTime.UtcNow,
                MontantTotal = montantTotal,
                MontantNuitee = montantNuitee,
                MontantRemise = montantRemise,
                MontantServices = 0, // A compléter si des services sont ajoutés
                Statut = StatutPaiement.EnAttente,
                LignesFacture = lignes
            };

            await _factureRepository.Add(facture);

            return new FactureDto

                (
                   facture.Id,
                   facture.ReservationId,
                   facture.DateEmission,
                   facture.MontantTotal,
                   facture.MontantNuitee,
                   facture.MontantRemise,
                   facture.MontantServices,
                   facture.Statut,
                   facture.LignesFacture.Select(l => new LigneFactureDto
                     (
                          l.Id,
                          l.Description,
                          l.Montant,
                          l.Quantite,
                          l.PrixUnitaire
                     )).ToList()
                );

        }

        private Season DeterminerSaison(DateOnly date)
        {
            var mois = date.Month;
            if (mois >= 7 && mois <= 8)
                return Season.HauteSaison;
            else if (mois == 12 || mois <= 2)
                return Season.PeriodeSpeciale;
            else
                return Season.BasseSaison;

        }


    }
}
