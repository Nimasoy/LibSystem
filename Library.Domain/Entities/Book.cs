using Library.Domain.Common;
using Library.Domain.Events;
using Library.Domain.ValueObjects.Book;

namespace Library.Domain.Entities
{
#nullable enable
    public class Book
    {
        private readonly List<DomainEvent> _domainEvents = new();

        public int Id { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Author { get; private set; } = string.Empty;
        public string ISBN { get; private set; } = string.Empty;
        public int TotalCopies { get; private set; }
        public int AvailableCopies { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public ICollection<BorrowRecord> BorrowRecords { get; private set; } = [];
        public ICollection<Reservation> Reservations { get; private set; } = [];
        // Added navigation properties for many-to-many relationships
        public ICollection<Category> Categories { get; private set; } = new List<Category>();
        public ICollection<Tag> Tags { get; private set; } = new List<Tag>();

        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        private Book() { } // For EF Core

        public Book(string title, string author, string isbn, int totalCopies)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Author = author ?? throw new ArgumentNullException(nameof(author));
            ISBN = isbn ?? throw new ArgumentNullException(nameof(isbn));
            TotalCopies = totalCopies;
            AvailableCopies = totalCopies;
        }

        public void UpdateDetails(string title, string author, string isbn)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Author = author ?? throw new ArgumentNullException(nameof(author));
            ISBN = isbn ?? throw new ArgumentNullException(nameof(isbn));
        }

        public void UpdateCopies(int totalCopies)
        {
            if (totalCopies < TotalCopies - AvailableCopies)
                throw new InvalidOperationException("Cannot reduce total copies below borrowed copies.");

            TotalCopies = totalCopies;
            AvailableCopies = totalCopies - (TotalCopies - AvailableCopies);
        }

        public bool IsAvailable() => AvailableCopies > 0;

        public bool IsReservedByOtherUser(int userId) => 
            Reservations.Any(r => r.UserId != userId && r.IsActive && !r.IsFulfilled);

        public void Borrow(User user)
        {
            if (!IsAvailable())
                throw new InvalidOperationException("No copies available for borrowing.");

            var borrowRecord = new BorrowRecord(this, user.Id);
            BorrowRecords.Add(borrowRecord);
            AvailableCopies--;

            _domainEvents.Add(new BookBorrowedEvent(Id, user.Id));
        }

        public void Return(User user)
        {
            var borrowRecord = BorrowRecords
                .FirstOrDefault(b => b.UserId == user.Id && !b.ReturnDate.HasValue);

            if (borrowRecord == null)
                throw new InvalidOperationException("No active borrow record found for this user.");

            borrowRecord.Return();
            AvailableCopies++;

            _domainEvents.Add(new BookReturnedEvent(Id, user.Id));
        }

        public void Reserve(User user)
        {
            if (IsAvailable())
                throw new InvalidOperationException("Book is available for borrowing. No need to reserve.");

            if (IsReservedByOtherUser(user.Id))
                throw new InvalidOperationException("Book is already reserved by another user.");

            var activeReservation = Reservations
                .FirstOrDefault(r => r.UserId == user.Id && r.IsActive && !r.IsFulfilled);

            if (activeReservation != null)
                throw new InvalidOperationException("User already has an active reservation for this book.");

            var reservation = new Reservation(this, user.Id);
            Reservations.Add(reservation);

            _domainEvents.Add(new BookReservedEvent(Id, user.Id, DateTime.UtcNow));
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
