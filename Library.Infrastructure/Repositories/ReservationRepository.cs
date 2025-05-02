using Library.Domain.Interfaces;
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
        public async Task AddAsync(Reservation reservation)
        {
            _reservation.Add(reservation);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Reservation reservation)
        {
            _reservation.Update(reservation);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Reservation reservation)
        {
            _reservation.Remove(reservation);
            await _context.SaveChangesAsync();
        }

        // domain specific queries
        public async Task<bool> IsReserved(int bookId)
        {
            return await _context.Reservations.AnyAsync(r => r.BookId == bookId);
        }
        public async Task<Reservation?> GetUserReservation(int userId, int bookId)
        {
            return await _context.Reservations
                .FirstOrDefaultAsync(r => r.UserId == userId && r.BookId == bookId);
        }

    }
}