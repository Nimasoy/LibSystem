using Library.Domain.Common;
using Library.Domain.DomainServices;

namespace Library.Domain.Interfaces;

public interface IDomainEventDispatcher
{
    Task DispatchEventsAsync(IEnumerable<DomainEvent> domainEvents);
    Task Dispatch(DomainEvent domainEvent);
    
} 