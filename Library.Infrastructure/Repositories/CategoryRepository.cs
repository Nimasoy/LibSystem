using Library.Domain.Interfaces;
using Library.Domain.Entities;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly LibraryDbContext _context;
        private readonly DbSet<Category> _category;
        public CategoryRepository(LibraryDbContext context)
        {
            _context = context;
            _category = _context.Set<Category>();
        }
        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _category.FindAsync(id);
        }
        public async Task<List<Category>> GetAllAsync()
        {
            return await _category.ToListAsync();
        }
        public async Task AddAsync(Category category)
        {
            _category.Add(category);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Category category)
        {
            _category.Update(category);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Category category)
        {
            _category.Remove(category);
            await _context.SaveChangesAsync();
        }

        // domain specific queries
        public async Task<bool> ExistsAsync(int categoryId)
        {
            return await _context.Categories.AnyAsync(c => c.Id == categoryId);
        }
        public async Task<Category?> GetByNameAsync(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Name.Value == name);
        }


    }
}