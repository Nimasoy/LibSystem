using Library.Domain.Common;
using Library.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Library.Infrastructure.Services;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator _mediator;
    private readonly ILogger<DomainEventDispatcher> _logger;

    public DomainEventDispatcher(IMediator mediator, ILogger<DomainEventDispatcher> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task DispatchEventsAsync(IEnumerable<DomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            try
            {
                _logger.LogInformation("Dispatching domain event {EventType}", domainEvent.GetType().Name);
                await _mediator.Publish(domainEvent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error dispatching domain event {EventType}", domainEvent.GetType().Name);
                throw;
            }
        }
    }

    public async Task Dispatch(DomainEvent domainEvent)
    {
        try
        {
            _logger.LogInformation("Dispatching domain event {EventType}", domainEvent.GetType().Name);
            await _mediator.Publish(domainEvent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error dispatching domain event {EventType}", domainEvent.GetType().Name);
            throw;
        }
    }
} 