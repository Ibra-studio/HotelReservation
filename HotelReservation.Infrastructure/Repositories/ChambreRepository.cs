using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Enums;
using HotelReservation.Domain.Interfaces;
using HotelReservation.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Infrastructure.Repositories
{
    public class ChambreRepository: IChambreRepository
    {
        private readonly AppDbContext _context;

        public ChambreRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Chambre?> GetById(Guid id)
        {
            return await _context.Chambres.FirstOrDefaultAsync(c=>c.Id== id);
        }
        public async Task<List<Chambre>> GetAll()
        {
            return await _context.Chambres.Include(c=>c.Equipements).ToListAsync();
        }

        public async Task<List<Chambre>> GetDisponibilite(DateOnly dateArrivee, DateOnly dateDepart)
        {
            var ChambresOccupeesIds = await _context.Reservations.Where(
                r => r.Statut != StatutReservation.Annulee
                && r.DateArrivee < dateDepart
                && r.DateDepart > dateArrivee)
                .Select(r => r.ChambreId)
                .ToListAsync();

            return await _context.Chambres.Include(c => c.Equipements).Where(
                 c => c.Statut == StatutChambre.Disponible
                 && !ChambresOccupeesIds.Contains(c.Id)
                ).ToListAsync();

        }
        public async Task Add(Chambre chambre)
        {
            await _context.Chambres.AddAsync(chambre);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Chambre chambre)
        {
            _context.Chambres.Update(chambre);
            await _context.SaveChangesAsync();
        }

    }

}
