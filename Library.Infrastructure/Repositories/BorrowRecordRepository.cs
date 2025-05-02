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
        private readonly DbSet<BorrowRecord> _records;

        public BorrowRecordRepository(LibraryDbContext context)
        {
            _context = context;
            _records = context.Set<BorrowRecord>();
        }

        public async Task<BorrowRecord?> GetByIdAsync(int id)
        {
            return await _records.FindAsync(id);
        }

        public async Task<List<BorrowRecord>> GetAllAsync()
        {
            return await _records.ToListAsync();
        }

        public async Task AddAsync(BorrowRecord borrow)
        {
            _records.Add(borrow);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BorrowRecord borrow)
        {
            _records.Update(borrow);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(BorrowRecord borrow)
        {
            _records.Remove(borrow);
            await _context.SaveChangesAsync();
        }

        // domain specific queries
        public async Task<List<BorrowRecord>> GetOverdueByUserId(int userId)
        {
            return await _context.BorrowRecords
                .Where(b => b.UserId == userId && b.ReturnedAt == null && b.DueAt < DateTime.UtcNow)
                .ToListAsync();
        }
        public async Task<bool> IsOverdue(int borrowRecordId)
        {
            var record = await _context.BorrowRecords.FindAsync(borrowRecordId);
            return record != null && record.ReturnedAt == null && record.DueAt < DateTime.UtcNow;
        }

    }
}
