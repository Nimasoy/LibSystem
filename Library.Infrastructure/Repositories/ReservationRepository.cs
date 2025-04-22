using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly LibraryDbContext _context;

        public ReservationRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Reservation?> GetByIdAsync(int id)
        {
            return await _context.Set<Reservation>().FindAsync(id);
        }

        public async Task<List<Reservation>> GetAllAsync()
        {
            return await _context.Set<Reservation>().ToListAsync();
        }

        public async Task AddAsync(Reservation entity)
        {
            _context.Set<Reservation>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Reservation entity)
        {
            _context.Set<Reservation>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Reservation entity)
        {
            _context.Set<Reservation>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}