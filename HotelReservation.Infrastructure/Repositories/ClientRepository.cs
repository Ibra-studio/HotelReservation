using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Interfaces;
using HotelReservation.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelReservation.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;

        }

        public async Task<List<Client>> GetAll()
        {
            return await  _context.Clients.ToListAsync();
        }

        public async Task<Client?> GetById(Guid id)
        {
            
            return await _context.Clients.FirstOrDefaultAsync(c=>c.Id == id);
        }
        public async Task<Client?> GetByNom(string nom)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Nom.ToLower() == nom.ToLower());
        }

        public async Task<Client?> GetByNumIdentite(string num)
        {
            return await _context.Clients.FirstOrDefaultAsync(c=> c.NumPieceIdentite == num);
        }

        public async Task Add(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Client client)
        {
             _context.Clients.Update(client);
            await _context.SaveChangesAsync();

        }
    }
}
