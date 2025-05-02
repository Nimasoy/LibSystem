using Library.Domain.Interfaces;
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

        public async Task AddAsync(Tag tag)
        {
            _tag.Add(tag);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tag tag)
        {
            _tag.Update(tag);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Tag tag)
        {
            _tag.Remove(tag);
            await _context.SaveChangesAsync();
        }

        // domain specific queries
        public async Task<Tag?> GetByNameAsync(string name)
        {
            return await _context.Tags.FirstOrDefaultAsync(t => t.Name.Value == name);
        }
        public async Task<bool> ExistsAsync(string name)
        {
            return await _context.Tags.AnyAsync(t => t.Name.Value == name);
        }

    }
}