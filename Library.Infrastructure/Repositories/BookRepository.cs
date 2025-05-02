using Library.Domain.Entities;
using Library.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Library.Domain.Interfaces;

namespace Library.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Tags)
                .Include(b => b.Categories)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Books
                .Include(b => b.Tags)
                .Include(b => b.Categories)
                .ToListAsync();
        }

        public async Task AddAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        // domain specific queries
        public async Task<IEnumerable<Book>> GetAvailableBooks()
        {
            return await _context.Books.Where(b => b.AvailableCopies > 0).ToListAsync();   
        }
        public async Task<bool> IsReservedByUser(int bookId, int userId)
        {
            return await _context.Reservations.AnyAsync(r => r.BookId == bookId && r.UserId == userId);
        }
        public async Task<bool> IsBookBorrowedByUser(int bookId, int userId)
        {
            return await _context.BorrowRecords.AnyAsync(b => b.BookId == bookId && b.UserId == userId);
        }
        public async Task<bool> ExistsAsync(string isbn)
        {
            return await _context.Books.AnyAsync(b => b.ISBN == isbn);
        }
        public async Task<IEnumerable<Book>> GetBooksByCategory(int categoryId)
        {
            return await _context.Books
                .Where(b => b.Categories.Any(c => c.Id == categoryId)).ToListAsync();
        }
        public async Task<IEnumerable<Book>> SearchByTitle(string keyword)
        {
            return await _context.Books
                .Where(b => b.Title.Contains(keyword)).ToListAsync();
        }
    }
}

