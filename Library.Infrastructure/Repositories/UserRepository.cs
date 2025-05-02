using Library.Domain.Interfaces;
using Library.Domain.Entities;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Library.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryDbContext _context;
        private readonly DbSet<User> _user;

        public UserRepository(LibraryDbContext context)
        {
            _context = context;
            _user = _context.Set<User>();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _user.FindAsync(id);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _user.ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            _user.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _user.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _user.Remove(user);
            await _context.SaveChangesAsync();
        }

        // domain specific queries
        public async Task<bool> HasOverdueBooks(int userId)
        {
            return await _context.BorrowRecords
                .AnyAsync(b => b.UserId == userId && b.ReturnedAt == null && b.DueAt < DateTime.UtcNow);
        }
        public async Task<bool> HasBorrowedBook(int userId, int bookId)
        {
            return await _context.BorrowRecords
                .AnyAsync(b => b.UserId == userId && b.BookId == bookId);
        }
        public async Task<bool> HasReservedBook(int userId, int bookId)
        {
            return await _context.Reservations
                .AnyAsync(r => r.UserId == userId && r.BookId == bookId);
        }

    }
}