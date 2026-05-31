using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Interfaces;
using HotelReservation.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Infrastructure.Repositories
{
    public class EquipementRepository : IEquipementRepository
    {
        private readonly AppDbContext _context;

        public EquipementRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Equipement>> GetAll()
        {
            return await _context.Equipements
                .ToListAsync();
        }

        public async Task<Equipement?> GetById(Guid id)
        {
            return await _context.Equipements
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Add(Equipement equipement)
        {
            await _context.Equipements.AddAsync(equipement);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Equipement equipement)
        {
            _context.Equipements.Update(equipement);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var equipement = await _context.Equipements
                .FirstOrDefaultAsync(e => e.Id == id);
            if (equipement == null) return;
            _context.Equipements.Remove(equipement);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Equipement>> GetByIds(List<Guid> ids)
        {
            return await _context.Equipements
                .Where(e => ids.Contains(e.Id))
                .ToListAsync();
        }
    }

}
