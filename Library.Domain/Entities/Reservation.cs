namespace Library.Domain.Entities
{
    public class Reservation(Book book)
    {
        public int Id { get; private set; }
        public int BookId { get; private set; }
        public int UserId { get; private set; }
        public DateTime ReservedAt { get; private set; }
        public Book Book { get; private set; } = book ?? throw new ArgumentNullException(nameof(book));
    }
}
