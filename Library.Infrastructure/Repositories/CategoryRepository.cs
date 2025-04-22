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

        public CategoryRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Set<Category>().FindAsync(id);
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Set<Category>().ToListAsync();
        }

        public async Task AddAsync(Category entity)
        {
            _context.Set<Category>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category entity)
        {
            _context.Set<Category>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category entity)
        {
            _context.Set<Category>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}