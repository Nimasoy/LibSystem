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
    public class BorrowRecordRepository : IBorrowRecordRepository
    {
        private readonly LibraryDbContext _context;

        public BorrowRecordRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<BorrowRecord?> GetByIdAsync(int id)
        {
            return await _context.Set<BorrowRecord>().FindAsync(id);
        }

        public async Task<List<BorrowRecord>> GetAllAsync()
        {
            return await _context.Set<BorrowRecord>().ToListAsync();
        }

        public async Task AddAsync(BorrowRecord entity)
        {
            _context.Set<BorrowRecord>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BorrowRecord entity)
        {
            _context.Set<BorrowRecord>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(BorrowRecord entity)
        {
            _context.Set<BorrowRecord>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
