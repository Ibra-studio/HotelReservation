using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Enums;
using HotelReservation.Domain.Interfaces;
using HotelReservation.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Infrastructure.Repositories
{
    public class TarifRepository : ITarifRepository
    {
        private readonly AppDbContext _context;

        public TarifRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tarif>> GetAll()
        {
            return await _context.Tarifs
                .ToListAsync();
        }

        public async Task<Tarif?> GetById(Guid id)
        {
            return await _context.Tarifs
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tarif?> GetByTypeAndSaison(TypeChambre type, Season saison)
        {
            return await _context.Tarifs
                .FirstOrDefaultAsync(t =>
                    t.TypeChambre == type &&
                    t.Saison == saison);
        }

        public async Task Add(Tarif tarif)
        {
            await _context.Tarifs.AddAsync(tarif);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Tarif tarif)
        {
            _context.Tarifs.Update(tarif);
            await _context.SaveChangesAsync();
        }


    }

}
