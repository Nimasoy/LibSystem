using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private readonly DbSet<Tag> _tag;

        public TagRepository(LibraryDbContext context)
        {
            _context = context;
            _tag = _context.Set<Tag>();
        }

        public async Task<Tag?> GetByIdAsync(int id)
        {
            return await _tag.FindAsync(id);
        }

        public async Task<List<Tag>> GetAllAsync()
        {
            return await _tag.ToListAsync();
        }

        public async Task AddAsync(Tag entity)
        {
            _tag.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tag entity)
        {
            _tag.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Tag entity)
        {
            _tag.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}