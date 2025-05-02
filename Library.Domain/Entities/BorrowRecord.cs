using Library.Domain.Common;

namespace Library.Domain.Entities;

public class BorrowRecord
{
    public int Id { get; private set; }
    public int BookId { get; private set; }
    public int UserId { get; private set; }
    public DateTime BorrowDate { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime? ReturnDate { get; private set; }
    public bool IsOverdue => !ReturnDate.HasValue && DateTime.UtcNow > DueDate;

    public Book Book { get; private set; } = null!;
    public User User { get; private set; } = null!;

    private BorrowRecord() { } // For EF Core

    public BorrowRecord(Book book, int userId, int borrowDurationDays = 14)
    {
        Book = book ?? throw new ArgumentNullException(nameof(book));
        BookId = book.Id;
        UserId = userId;
        BorrowDate = DateTime.UtcNow;
        DueDate = BorrowDate.AddDays(borrowDurationDays);
    }

    public void Return()
    {
        if (ReturnDate.HasValue)
            throw new InvalidOperationException("Book has already been returned.");

        ReturnDate = DateTime.UtcNow;
    }
}
