namespace Library.Domain.Entities
{
    public class Reservation(Book book, int userId)
    {
        public int Id { get; private set; } 
        public int BookId { get; private set; } = book.Id;
        public int UserId { get; private set; } = userId;
        public DateTime ReservedAt { get; private set; } = DateTime.UtcNow;
        public Book Book { get; private set; } = book;
    }
}
