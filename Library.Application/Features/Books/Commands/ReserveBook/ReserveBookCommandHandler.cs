using Library.Application.Common;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Library.Application.Features.Books.Commands.ReserveBook;

public record ReserveBookCommand(int BookId, int UserId) : IRequest<BaseResponse>;

public class ReserveBookCommandHandler : IRequestHandler<ReserveBookCommand, BaseResponse>
{
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    private readonly IDomainEventDispatcher _domainEventDispatcher;
    private readonly ILogger<ReserveBookCommandHandler> _logger;

    public ReserveBookCommandHandler(
        IBookRepository bookRepository,
        IUserRepository userRepository,
        IDomainEventDispatcher domainEventDispatcher,
        ILogger<ReserveBookCommandHandler> logger)
    {
        _bookRepository = bookRepository;
        _userRepository = userRepository;
        _domainEventDispatcher = domainEventDispatcher;
        _logger = logger;
    }

    public async Task<BaseResponse> Handle(ReserveBookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var book = await _bookRepository.GetByIdAsync(request.BookId);
            if (book == null)
                return new BaseResponse { Success = false, Message = "Book not found" };

            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
                return new BaseResponse { Success = false, Message = "User not found" };

            if (book.IsAvailable())
                return new BaseResponse { Success = false, Message = "Book is available for borrowing" };

            if (user.Reservations.Any(r => r.BookId == book.Id && r.IsActive && !r.IsFulfilled))
                return new BaseResponse { Success = false, Message = "User already has an active reservation for this book" };

            book.Reserve(user);
            await _bookRepository.UpdateAsync(book);
            await _domainEventDispatcher.DispatchEventsAsync(book.DomainEvents);
            book.ClearDomainEvents();

            _logger.LogInformation("Book {BookId} reserved by user {UserId}", request.BookId, request.UserId);

            return new BaseResponse { Success = true, Message = "Book reserved successfully" };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reserving book {BookId} for user {UserId}", request.BookId, request.UserId);
            return new BaseResponse { Success = false, Message = "An error occurred while reserving the book" };
        }
    }
}