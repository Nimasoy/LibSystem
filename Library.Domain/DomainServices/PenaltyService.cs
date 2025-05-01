using Library.Domain.Entities;

namespace Library.Domain.DomainServices
{
    public record PenaltyCalculatedEvent(int UserId, int BookId, int DaysOverdue, int TotalFine);

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

            foreach (var record in user.Borrows)
            {
                if (record.ReturnedAt == null && record.DueAt < DateTime.UtcNow)
                {
                    int daysOverdue = (DateTime.UtcNow - record.DueAt).Days;
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
