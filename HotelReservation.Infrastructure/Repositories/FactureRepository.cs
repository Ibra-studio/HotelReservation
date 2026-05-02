using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Interfaces;
using HotelReservation.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Infrastructure.Repositories
{
    public class FactureRepository :IFactureRepository
    {
        private readonly AppDbContext _context;

        public FactureRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Facture>> GetAll()
        {
            return await _context.Factures.ToListAsync();
        }
        public async Task<Facture?> GetById(Guid id)
        {
            return await _context.Factures.Include(f=>f.LignesFacture).Include(f=>f.Reservation).FirstOrDefaultAsync(f=>f.Id == id);
        }

        public async Task<Facture?> GetByReservationId(Guid reservationId)
        {
            return await _context.Factures.Include(f => f.LignesFacture).FirstOrDefaultAsync(f => f.ReservationId == reservationId);
        }
        public async Task<List<Facture>> GetByClientId(Guid clientId)
        {
           
            return await _context.Factures.Include(f => f.Reservation).Include(f => f.LignesFacture).Where(f =>
            f.Reservation!=null &&
            f.Reservation.ClientId==clientId)
             .ToListAsync();
        }
        public async Task Add(Facture facture)
        {
            await _context.Factures.AddAsync(facture);
            await _context.SaveChangesAsync();
        }


    }
}
