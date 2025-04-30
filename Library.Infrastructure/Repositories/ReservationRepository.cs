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
        private readonly DbSet<Reservation> _reservation;

        public ReservationRepository(LibraryDbContext context)
        {
            _context = context;
            _reservation = _context.Set<Reservation>();
        }

        public async Task<Reservation?> GetByIdAsync(int id)
        {
            return await _reservation.FindAsync(id);
        }

        public async Task<List<Reservation>> GetAllAsync()
        {
            return await _reservation.ToListAsync();
        }

        public async Task AddAsync(Reservation entity)
        {
            _reservation.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Reservation entity)
        {
            _reservation.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Reservation entity)
        {
            _reservation.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}