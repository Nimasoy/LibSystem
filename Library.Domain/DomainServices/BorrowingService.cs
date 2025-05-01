using Library.Domain.Entities;
using Library.Domain.ValueObjects.Book;

namespace Library.Domain.DomainServices
{
    public record BookBorrowedEvent(int UserId, int BookId, DateTime BorrowedAt);
    public class BorrowingService
    {
        private readonly IDomainEventDispatcher _eventDispatcher;

        public BorrowingService(IDomainEventDispatcher eventDispatcher)
        {
            _eventDispatcher = eventDispatcher;
        }
        private const int BorrowLimit = 5;
        public void BorrowBook(User user, Book book)
        {
            if (user.Borrows.Count >= BorrowLimit) throw new InvalidOperationException("User has reached the borrow limit.");
            if (user.Borrows.Any(b => b.IsOverdue)) throw new InvalidOperationException("User has overdue books.");
            if (!book.IsAvailable()) throw new InvalidOperationException("No copies are available.");
            if (book.IsReservedByOtherUser(user.Id)) throw new InvalidOperationException("Book is reserved by another user.");

            book.Borrow();
            user.AddBorrowRecord(book);

            //remove reservation when borrowing is done
            var reservation = book.Reservations.FirstOrDefault(r => r.UserId == user.Id); 
            if (reservation is not null)
            {
                book.Reservations.Remove(reservation);
            }

            _eventDispatcher.Dispatch(new BookBorrowedEvent(user.Id, book.Id, DateTime.UtcNow));
        }
    }
}
