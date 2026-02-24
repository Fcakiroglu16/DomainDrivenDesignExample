#region

using MediatR;

#endregion

namespace DomainDrivenDesignExample.API.SharedKernels;

public interface IDomainEvent : INotification;

public interface IIntegrationEvent : INotification;