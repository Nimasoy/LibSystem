using Library.Application.Common;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

// Removed duplicate command definition
// using Library.Application.Features.Books.Commands.ReturnBook; // Assuming ReturnBookCommand is in this namespace or accessible

namespace Library.Application.Features.Books.Commands.ReturnBook;

public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand, BaseResponse>
{
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    private readonly IBorrowRecordRepository _borrowRecordRepository;
    private readonly IDomainEventDispatcher _domainEventDispatcher;
    private readonly ILogger<ReturnBookCommandHandler> _logger;

    public ReturnBookCommandHandler(
        IBookRepository bookRepository,
        IUserRepository userRepository,
        IBorrowRecordRepository borrowRecordRepository,
        IDomainEventDispatcher domainEventDispatcher,
        ILogger<ReturnBookCommandHandler> logger)
    {
        _bookRepository = bookRepository;
        _userRepository = userRepository;
        _borrowRecordRepository = borrowRecordRepository;
        _domainEventDispatcher = domainEventDispatcher;
        _logger = logger;
    }

    public async Task<BaseResponse> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var book = await _bookRepository.GetByIdAsync(request.BookId);
            if (book == null)
                return new BaseResponse { Success = false, Message = "Book not found" };

            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
                return new BaseResponse { Success = false, Message = "User not found" };

            var borrowRecord = await _borrowRecordRepository.GetActiveBorrowForUserAndBook(request.UserId, request.BookId);
            if (borrowRecord == null)
                return new BaseResponse { Success = false, Message = "No active borrow record found for this book" };

            // Corrected method name from ReturnBook(user) to Return(user)
            book.Return(user);
            // Corrected method name from MarkAsReturned() to Return()
            borrowRecord.Return();

            await _bookRepository.UpdateAsync(book);
            await _borrowRecordRepository.UpdateAsync(borrowRecord);

            await _domainEventDispatcher.DispatchEventsAsync(book.DomainEvents);
            book.ClearDomainEvents();

            _logger.LogInformation("Book {BookId} returned by user {UserId}", request.BookId, request.UserId);

            return new BaseResponse { Success = true, Message = "Book returned successfully" };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error returning book {BookId} for user {UserId}", request.BookId, request.UserId);
            return new BaseResponse { Success = false, Message = "An error occurred while returning the book" };
        }
    }
} 