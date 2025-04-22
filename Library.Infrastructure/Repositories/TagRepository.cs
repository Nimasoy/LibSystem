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
    public class TagRepository : ITagRepository
    {
        private readonly LibraryDbContext _context;

        public TagRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Tag?> GetByIdAsync(int id)
        {
            return await _context.Set<Tag>().FindAsync(id);
        }

        public async Task<List<Tag>> GetAllAsync()
        {
            return await _context.Set<Tag>().ToListAsync();
        }

        public async Task AddAsync(Tag entity)
        {
            _context.Set<Tag>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tag entity)
        {
            _context.Set<Tag>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Tag entity)
        {
            _context.Set<Tag>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}