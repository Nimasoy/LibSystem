using Library.Domain.ValueObjects.Book;

namespace Library.Domain.Entities
{
#nullable enable
    public class Book(Title title, Author author, ISBN iSBN, Publisher publisher, Year year,
        TotalCopies totalCopies, AvailableCopies availableCopies, Category category) // new c#12 feature :)
    {
        public int Id { get; private set; }
        public Title Title { get; private set; } = title ?? throw new ArgumentNullException(nameof(title));
        public Author Author { get; private set; } = author ?? throw new ArgumentNullException(nameof(author));
        public ISBN ISBN { get; private set; } = iSBN ?? throw new ArgumentNullException(nameof(iSBN));
        public Publisher Publisher { get; private set; } = publisher ?? throw new ArgumentNullException(nameof(publisher));
        public Year Year { get; private set; } = year ?? throw new ArgumentNullException(nameof(year));
        public TotalCopies TotalCopies { get; private set; } = totalCopies ?? throw new ArgumentNullException(nameof(totalCopies));
        public AvailableCopies AvailableCopies { get; private set; } = availableCopies ?? throw new ArgumentNullException(nameof(availableCopies));
        public Category Category { get; private set; } = category ?? throw new ArgumentNullException(nameof(category));
        public int CategoryId { get; private set; }

        public ICollection<Tag> Tags { get; private set; } = [];
        public ICollection<Reservation> Reservations { get; private set; } = [];
        public ICollection<BorrowRecord> Borrows { get; private set; } = [];

        public void Borrow()
        {
            if (AvailableCopies.Value <= 0)
                throw new InvalidOperationException("No copies available.");

            AvailableCopies = new AvailableCopies(AvailableCopies.Value - 1);
        }
        public void ReturnBook()
        {
            if (AvailableCopies.Value >= TotalCopies.Value) throw new InvalidOperationException("All copies are already returned.");
            AvailableCopies = new AvailableCopies(AvailableCopies.Value + 1);
        }
        public bool IsAvailable() => AvailableCopies.Value > 0;
        public bool IsReservedByOtherUser(int userId)
        {
            return Reservations.Any(r => r.UserId != userId);
        } 
        public void AddTag(Tag tag)
        {
            if (Tags.Any(t => t.Id == tag.Id)) throw new InvalidOperationException("Tag already exists.");
            Tags.Add(tag);
        }
    }
}
