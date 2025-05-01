namespace Library.Domain.Entities
{
    public class BorrowRecord(Book book)
    {
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public int BookId { get; private set; }

        public DateTime BorrowedAt { get; private set; }
        public DateTime DueAt { get; private set; }
        public DateTime? ReturnedAt { get; private set; }
        public Book Book { get; private set; } = book ?? throw new ArgumentNullException(nameof(book));

        public bool IsOverdue => ReturnedAt == null && DueAt < DateTime.Now;
    }
}
