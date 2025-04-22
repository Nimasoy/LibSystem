using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Domain.Entities;

namespace Library.Application.Interfaces.Repositories
{
    public interface ITagRepository
    {
        Task<Tag?> GetByIdAsync(int id);
        Task<List<Tag>> GetAllAsync();
        Task AddAsync(Tag entity);
        Task UpdateAsync(Tag entity);
        Task DeleteAsync(Tag entity);
    }
}