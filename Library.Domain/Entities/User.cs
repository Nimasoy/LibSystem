using Library.Domain.ValueObjects.User;

namespace Library.Domain.Entities
{
    public class User(FullName fullname, Email email)
    {
        public int Id { get; private set; }
        public FullName FullName { get; private set; } = fullname ?? throw new ArgumentNullException(nameof(fullname));
        public Email Email { get; private set; } = email ?? throw new ArgumentNullException(nameof(email));

        public ICollection<BorrowRecord> Borrows { get; private set; } = [];
        public ICollection<Reservation> Reservations { get; private set; } = [];
    }
}