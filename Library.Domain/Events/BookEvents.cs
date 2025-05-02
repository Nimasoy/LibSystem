using Library.Domain.Common;
using Library.Domain.Entities;

namespace Library.Domain.Events;

public class BookBorrowedEvent : DomainEvent
{
    public int BookId { get; }
    public int UserId { get; }
    public DateTime BorrowedAt { get; }

    public BookBorrowedEvent(int bookId, int userId)
    {
        BookId = bookId;
        UserId = userId;
        BorrowedAt = DateTime.UtcNow;
    }
}

public class BookReturnedEvent : DomainEvent
{
    public int BookId { get; }
    public int UserId { get; }
    public DateTime ReturnedAt { get; }

    public BookReturnedEvent(int bookId, int userId)
    {
        BookId = bookId;
        UserId = userId;
        ReturnedAt = DateTime.UtcNow;
    }
}

public class BookReservedEvent : DomainEvent
{
    public int BookId { get; }
    public int UserId { get; }
    public DateTime ReservedAt { get; }

    public BookReservedEvent(int bookId, int userId, DateTime reservedAt)
    {
        BookId = bookId;
        UserId = userId;
        ReservedAt = reservedAt;
    }
} 