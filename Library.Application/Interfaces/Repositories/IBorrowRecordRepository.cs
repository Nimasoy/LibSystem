using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Domain.Entities;

namespace Library.Application.Interfaces.Repositories
{
    public interface IBorrowRecordRepository
    {
        Task<BorrowRecord?> GetByIdAsync(int id);
        Task<List<BorrowRecord>> GetAllAsync();
        Task AddAsync(BorrowRecord entity);
        Task UpdateAsync(BorrowRecord entity);
        Task DeleteAsync(BorrowRecord entity);
    }
}
