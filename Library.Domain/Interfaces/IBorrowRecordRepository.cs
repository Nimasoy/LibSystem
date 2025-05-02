using Library.Domain.Entities;

namespace Library.Domain.Interfaces
{
    public interface IBorrowRecordRepository
    {
        Task<BorrowRecord?> GetByIdAsync(int id);
        Task<IEnumerable<BorrowRecord>> GetAllAsync();
        Task<IEnumerable<BorrowRecord>> GetActiveBorrowsForUser(int userId);
        Task AddAsync(BorrowRecord borrow);
        Task UpdateAsync(BorrowRecord borrow);
        Task DeleteAsync(BorrowRecord borrow);

        Task<List<BorrowRecord>> GetOverdueByUserId(int userId);
        Task<bool> IsOverdue(int borrowRecordId);
        // Added missing method signature
        Task<BorrowRecord?> GetActiveBorrowForUserAndBook(int userId, int bookId);
        // Added missing method signature for getting all overdue borrows
        Task<IEnumerable<BorrowRecord>> GetOverdueBorrows();
    }
}
