namespace Library.Domain.Entities
{
    public class BorrowRecord(Book book, int userId)
    {
        public int Id { get; private set; }
        public int UserId { get; private set; } = userId;
        public int BookId { get; private set; } = book.Id;
        public DateTime BorrowedAt { get; private set; } = DateTime.UtcNow;
        public DateTime DueAt { get; private set; } = DateTime.UtcNow.AddDays(14);
        public DateTime? ReturnedAt { get; private set; }
        public Book Book { get; private set; } = book;

        public bool IsOverdue => ReturnedAt == null && DueAt < DateTime.Now;
    }
}
