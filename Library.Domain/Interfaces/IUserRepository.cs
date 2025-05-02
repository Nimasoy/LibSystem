using Library.Domain.Entities;

namespace Library.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<List<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);  
        Task DeleteAsync(User user);

        Task<bool> HasOverdueBooks(int userId);
        Task<bool> HasBorrowedBook(int userId, int bookId);
        Task<bool> HasReservedBook(int userId, int bookId);

    }
}