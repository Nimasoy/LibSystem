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
    public class UserRepository : IUserRepository
    {
        private readonly LibraryDbContext _context;
        private readonly DbSet<User> _user;

        public UserRepository(LibraryDbContext context)
        {
            _context = context;
            _user = _context.Set<User>();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _user.FindAsync(id);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _user.ToListAsync();
        }

        public async Task AddAsync(User entity)
        {
            _user.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User entity)
        {
            _user.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User entity)
        {
            _user.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}