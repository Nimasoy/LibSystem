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
        private readonly DbSet<User> _users;

        public UserRepository(LibraryDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _users = _context.Set<User>();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _users.FindAsync(id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _users.FirstOrDefaultAsync(u => u.Email == email);
        }

        // Changed return type to match the interface
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _users.ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            await _users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _users.Update(user);
            await _context.SaveChangesAsync();
        }

        // Changed parameter to match the interface and implemented deletion by ID
        public async Task DeleteAsync(int id)
        {
            var userToDelete = await _users.FindAsync(id);
            if (userToDelete != null)
            {
                _users.Remove(userToDelete);
                await _context.SaveChangesAsync();
            }
        }

        // domain specific queries
        public async Task<bool> HasOverdueBooks(int userId)
        {
            return await _context.BorrowRecords
                .AnyAsync(b => b.UserId == userId && b.ReturnDate == null && b.DueDate < DateTime.UtcNow);
        }
        public async Task<bool> HasBorrowedBook(int userId, int bookId)
        {
            return await _context.BorrowRecords
                .AnyAsync(b => b.UserId == userId && b.BookId == bookId && b.ReturnDate == null);
        }
        public async Task<bool> HasReservedBook(int userId, int bookId)
        {
            return await _context.Reservations
                .AnyAsync(r => r.UserId == userId && r.BookId == bookId && r.IsActive && !r.IsFulfilled);
        }

    }
}