using Library.Domain.Common;
using Library.Domain.Entities;
using Library.Domain.Interfaces;

namespace Library.Domain.DomainServices
{
    public class PenaltyCalculatedEvent : DomainEvent
    {
        public int UserId { get; }
        public int BookId { get; }
        public int DaysOverdue { get; }
        public int TotalFine { get; }

        public PenaltyCalculatedEvent(int userId, int bookId, int daysOverdue, int totalFine)
        {
            UserId = userId;
            BookId = bookId;
            DaysOverdue = daysOverdue;
            TotalFine = totalFine;
        }
    }

    public class PenaltyService
    {   
        private readonly IDomainEventDispatcher _eventDispatcher;
        public PenaltyService(IDomainEventDispatcher eventDispatcher)
        {
            _eventDispatcher = eventDispatcher;
        }
        private const int PenaltyAmountPerDay = 10;
        public Dictionary<int, int> CalculatePenalty(User user)
        {   
            var penalties = new Dictionary<int, int>();

            foreach (var record in user.BorrowRecords)
            {
                if (record.ReturnDate == null && record.DueDate < DateTime.UtcNow)
                {
                    int daysOverdue = (DateTime.UtcNow - record.DueDate).Days;
                    int totalAmount = daysOverdue * PenaltyAmountPerDay;
                    penalties[record.BookId] = totalAmount;

                    _eventDispatcher.Dispatch(new PenaltyCalculatedEvent(
                    user.Id, record.BookId, daysOverdue, totalAmount));
                }
            }
            return penalties;
        }
    }
}
