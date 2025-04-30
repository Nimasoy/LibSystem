using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Application.Interfaces.Repositories;
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

        public async Task AddAsync(BorrowRecord entity)
        {
            _records.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BorrowRecord entity)
        {
            _records.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(BorrowRecord entity)
        {
            _records.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
