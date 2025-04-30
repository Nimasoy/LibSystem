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

        public async Task AddAsync(Category entity)
        {
            _category.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category entity)
        {
            _category.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category entity)
        {
            _category.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}