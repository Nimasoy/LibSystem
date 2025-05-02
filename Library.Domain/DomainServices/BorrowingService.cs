using Library.Domain.Common;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Library.Domain.DomainServices;

public class BorrowingService
{
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    private readonly IDomainEventDispatcher _domainEventDispatcher;
    private readonly ILogger<BorrowingService> _logger;
    private const int MaxBorrowsPerUser = 5;

    public BorrowingService(
        IBookRepository bookRepository,
        IUserRepository userRepository,
        IDomainEventDispatcher domainEventDispatcher,
        ILogger<BorrowingService> logger)
    {
        _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _domainEventDispatcher = domainEventDispatcher ?? throw new ArgumentNullException(nameof(domainEventDispatcher));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> BorrowBook(int bookId, int userId)
    {
        try
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null)
            {
                _logger.LogWarning("Book {BookId} not found", bookId);
                return false;
            }

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("User {UserId} not found", userId);
                return false;
            }

            if (!book.IsAvailable())
            {
                _logger.LogWarning("Book {BookId} is not available", bookId);
                return false;
            }

            if (user.BorrowRecords.Any(b => b.BookId == book.Id && !b.ReturnDate.HasValue))
            {
                _logger.LogWarning("User {UserId} has already borrowed book {BookId}", userId, bookId);
                return false;
            }

            if (!await CanUserBorrowMoreBooks(userId))
            {
                _logger.LogWarning("User {UserId} has reached the maximum number of borrowed books", userId);
                return false;
            }

            book.Borrow(user);
            await _bookRepository.UpdateAsync(book);
            await _domainEventDispatcher.DispatchEventsAsync(book.DomainEvents);
            book.ClearDomainEvents();

            _logger.LogInformation("Book {BookId} borrowed by user {UserId}", bookId, userId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error borrowing book {BookId} for user {UserId}", bookId, userId);
            return false;
        }
    }

    public async Task<bool> ReturnBook(int bookId, int userId)
    {
        try
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null)
            {
                _logger.LogWarning("Book {BookId} not found", bookId);
                return false;
            }

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("User {UserId} not found", userId);
                return false;
            }

            book.Return(user);
            await _bookRepository.UpdateAsync(book);
            await _domainEventDispatcher.DispatchEventsAsync(book.DomainEvents);
            book.ClearDomainEvents();

            _logger.LogInformation("Book {BookId} returned by user {UserId}", bookId, userId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error returning book {BookId} for user {UserId}", bookId, userId);
            return false;
        }
    }

    public async Task<bool> ReserveBook(int bookId, int userId)
    {
        try
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null)
            {
                _logger.LogWarning("Book {BookId} not found", bookId);
                return false;
            }

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("User {UserId} not found", userId);
                return false;
            }

            if (book.IsAvailable())
            {
                _logger.LogWarning("Book {BookId} is available, no need to reserve", bookId);
                return false;
            }

            if (book.IsReservedByOtherUser(userId))
            {
                _logger.LogWarning("Book {BookId} is already reserved by another user", bookId);
                return false;
            }

            book.Reserve(user);
            await _bookRepository.UpdateAsync(book);
            await _domainEventDispatcher.DispatchEventsAsync(book.DomainEvents);
            book.ClearDomainEvents();

            _logger.LogInformation("Book {BookId} reserved by user {UserId}", bookId, userId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reserving book {BookId} for user {UserId}", bookId, userId);
            return false;
        }
    }

    public async Task<IEnumerable<BorrowRecord>> GetUserActiveBorrows(int userId)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("User {UserId} not found", userId);
                return Enumerable.Empty<BorrowRecord>();
            }

            return user.BorrowRecords.Where(b => !b.ReturnDate.HasValue);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting active borrows for user {UserId}", userId);
            return Enumerable.Empty<BorrowRecord>();
        }
    }

    private async Task<bool> CanUserBorrowMoreBooks(int userId)
    {
        try
        {
            var activeBorrows = await GetUserActiveBorrows(userId);
            return activeBorrows.Count() < MaxBorrowsPerUser;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if user {UserId} can borrow more books", userId);
            return false;
        }
    }
}