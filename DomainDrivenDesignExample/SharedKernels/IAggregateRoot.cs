using DomainDrivenDesignExample.API.SharedKernels;

namespace CinemaTicketingSystem.SharedKernel.AggregateRoot;

public interface IAggregateRoot
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }


    IReadOnlyCollection<IIntegrationEvent> IntegrationEvents { get; }

    void ClearDomainEvents();
    void AddDomainEvent(IDomainEvent eventData);

    void ClearIntegrationEvents();
    void AddIntegrationEvent(IIntegrationEvent eventData);
}