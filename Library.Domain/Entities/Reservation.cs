using Library.Domain.Common;

namespace Library.Domain.Entities;

public class Reservation
{
    public int Id { get; private set; }
    public int BookId { get; private set; }
    public int UserId { get; private set; }
    public DateTime ReservationDate { get; private set; }
    public DateTime ExpiryDate { get; private set; }
    public bool IsActive => DateTime.UtcNow <= ExpiryDate;
    public bool IsFulfilled { get; private set; }

    public Book Book { get; private set; } = null!;
    public User User { get; private set; } = null!;

    private Reservation() { } // For EF Core

    public Reservation(Book book, int userId, int reservationDurationDays = 7)
    {
        Book = book ?? throw new ArgumentNullException(nameof(book));
        BookId = book.Id;
        UserId = userId;
        ReservationDate = DateTime.UtcNow;
        ExpiryDate = ReservationDate.AddDays(reservationDurationDays);
    }

    public void Fulfill()
    {
        if (IsFulfilled)
            throw new InvalidOperationException("Reservation has already been fulfilled.");

        if (!IsActive)
            throw new InvalidOperationException("Reservation has expired.");

        IsFulfilled = true;
    }
}
