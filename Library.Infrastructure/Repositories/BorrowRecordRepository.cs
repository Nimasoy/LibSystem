using Library.Domain.Interfaces;
using Library.Domain.Entities;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Library.Infrastructure.Repositories
{
    public class BorrowRecordRepository : IBorrowRecordRepository
    {
        private readonly LibraryDbContext _context;

        public BorrowRecordRepository(LibraryDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<BorrowRecord?> GetByIdAsync(int id)
        {
            return await _context.BorrowRecords
                .Include(b => b.Book)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<BorrowRecord>> GetAllAsync()
        {
            return await _context.BorrowRecords
                .Include(b => b.Book)
                .Include(b => b.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<BorrowRecord>> GetActiveBorrowsForUser(int userId)
        {
            return await _context.BorrowRecords
                .Include(b => b.Book)
                .Include(b => b.User)
                .Where(b => b.UserId == userId && b.ReturnDate == null)
                .ToListAsync();
        }

        public async Task<IEnumerable<BorrowRecord>> GetOverdueBorrows()
        {
            return await _context.BorrowRecords
                .Include(b => b.Book)
                .Include(b => b.User)
                .Where(b => b.ReturnDate == null && b.DueDate < DateTime.UtcNow)
                .ToListAsync();
        }

        public async Task AddAsync(BorrowRecord borrowRecord)
        {
            await _context.BorrowRecords.AddAsync(borrowRecord);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BorrowRecord borrowRecord)
        {
            _context.BorrowRecords.Update(borrowRecord);
            await _context.SaveChangesAsync();
        }

        // Modified DeleteAsync to match the interface signature
        public async Task DeleteAsync(BorrowRecord borrowRecord)
        {
            _context.BorrowRecords.Remove(borrowRecord);
            await _context.SaveChangesAsync();
        }

        // Added missing method implementation from IBorrowRecordRepository
        public async Task<BorrowRecord?> GetActiveBorrowForUserAndBook(int userId, int bookId)
        {
            return await _context.BorrowRecords
                .Include(b => b.Book)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.UserId == userId && b.BookId == bookId && b.ReturnDate == null);
        }

        // domain specific queries
        public async Task<List<BorrowRecord>> GetOverdueByUserId(int userId)
        {
            return await _context.BorrowRecords
                // Corrected property names from ReturnedAt to ReturnDate and DueAt to DueDate
                .Where(b => b.UserId == userId && b.ReturnDate == null && b.DueDate < DateTime.UtcNow)
                .ToListAsync();
        }
        public async Task<bool> IsOverdue(int borrowRecordId)
        {
            var record = await _context.BorrowRecords.FindAsync(borrowRecordId);
            // Corrected property names from ReturnedAt to ReturnDate and DueAt to DueDate
            return record != null && record.ReturnDate == null && record.DueDate < DateTime.UtcNow;
        }
    }
}
