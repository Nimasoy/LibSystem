using Library.Application.Common;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Library.Application.Features.Books.Commands.BorrowBook;

public record BorrowBookCommand(int BookId, int UserId) : IRequest<BaseResponse>;

public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand, BaseResponse>
{
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    private readonly IDomainEventDispatcher _domainEventDispatcher;
    private readonly ILogger<BorrowBookCommandHandler> _logger;

    public BorrowBookCommandHandler(
        IBookRepository bookRepository,
        IUserRepository userRepository,
        IDomainEventDispatcher domainEventDispatcher,
        ILogger<BorrowBookCommandHandler> logger)
    {
        _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _domainEventDispatcher = domainEventDispatcher ?? throw new ArgumentNullException(nameof(domainEventDispatcher));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<BaseResponse> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var book = await _bookRepository.GetByIdAsync(request.BookId);
            if (book == null)
                return new BaseResponse { Success = false, Message = "Book not found" };

            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
                return new BaseResponse { Success = false, Message = "User not found" };

            if (!book.IsAvailable())
                return new BaseResponse { Success = false, Message = "Book is not available" };

            if (user.BorrowRecords.Any(b => b.BookId == book.Id && !b.ReturnDate.HasValue))
                return new BaseResponse { Success = false, Message = "User has already borrowed this book" };

            book.Borrow(user);
            await _bookRepository.UpdateAsync(book);
            await _domainEventDispatcher.DispatchEventsAsync(book.DomainEvents);
            book.ClearDomainEvents();

            _logger.LogInformation("Book {BookId} borrowed by user {UserId}", request.BookId, request.UserId);

            return new BaseResponse { Success = true, Message = "Book borrowed successfully" };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error borrowing book {BookId} for user {UserId}", request.BookId, request.UserId);
            return new BaseResponse { Success = false, Message = "An error occurred while borrowing the book" };
        }
    }
}