using Library.Domain.Entities;

namespace Library.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<Book?> GetByIdAsync(int id);
        Task<List<Book>> GetAllAsync();
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(Book book);

        Task<IEnumerable<Book>> GetAvailableBooks();
        Task<bool> IsReservedByUser(int bookId, int userId);
        Task<bool> IsBookBorrowedByUser(int bookId, int userId);
        Task<IEnumerable<Book>> GetBooksByCategory(int categoryId);
        Task<IEnumerable<Book>> SearchByTitle(string keyword);
    }
}

