using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Interfaces;
using HotelReservation.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly AppDbContext _context;

        public ReservationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Reservation?> GetById(Guid id)
        {
            return await _context.Reservations
                .Include(r => r.Client)
                .Include(r => r.Chambre)
                .Include(r => r.Facture)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Reservation>> GetByClientId(Guid clientId)
        {
            return await _context.Reservations
                .Include(r => r.Chambre)
                .Include(r => r.Facture)
                .Where(r => r.ClientId == clientId)
                .ToListAsync();
        }

        public async Task Add(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }
       
    }

}
