using Library.Domain.Entities;

namespace Library.Domain.Interfaces
{
    public interface IBorrowRecordRepository
    {
        Task<BorrowRecord?> GetByIdAsync(int id);
        Task<List<BorrowRecord>> GetAllAsync();
        Task AddAsync(BorrowRecord borrow);
        Task UpdateAsync(BorrowRecord borrow);
        Task DeleteAsync(BorrowRecord borrow);

        Task<List<BorrowRecord>> GetOverdueByUserId(int userId);
        Task<bool> IsOverdue(int borrowRecordId);
    }
}
