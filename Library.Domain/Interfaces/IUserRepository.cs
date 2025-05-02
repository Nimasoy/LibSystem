using Library.Domain.Entities;

namespace Library.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);

        Task<bool> HasOverdueBooks(int userId);
        Task<bool> HasBorrowedBook(int userId, int bookId);
        Task<bool> HasReservedBook(int userId, int bookId);
    }
}