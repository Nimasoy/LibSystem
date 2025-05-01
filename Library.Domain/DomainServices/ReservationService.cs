using Library.Domain.Entities;

namespace Library.Domain.DomainServices;

public record BookReservedEvent(int UserId, int BookId, DateTime ReservedAt);

public class ReservationService
{
    private readonly IDomainEventDispatcher _eventDispatcher;

    public ReservationService(IDomainEventDispatcher eventDispatcher)
    {
        _eventDispatcher = eventDispatcher;
    }
    public void ReserveBook(User user, Book book)
    {
        if (user.Borrows.Any(b => b.BookId == book.Id))
            throw new InvalidOperationException("User has already borrowed this book.");

        if (user.Reservations.Any(r => r.BookId == book.Id))
            throw new InvalidOperationException("User has already reserved this book.");

        if (book.IsAvailable())
            throw new InvalidOperationException("Book is available. Borrow instead of reserving.");

        var reservation = new Reservation(book, user.Id);

        user.Reservations.Add(reservation);
        book.Reservations.Add(reservation);

        _eventDispatcher.Dispatch(new BookReservedEvent(user.Id, book.Id, DateTime.UtcNow));

    }
}
