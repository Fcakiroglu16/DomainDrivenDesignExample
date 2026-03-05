using CinemaTicketingSystem.Domain.BoundedContexts.Ticketing.ValueObjects;
using CinemaTicketingSystem.SharedKernel.AggregateRoot;
using DomainDrivenDesignExample.API.SharedKernels.ValueObjects;

namespace DomainDrivenDesignExample.API.BoundedContexts.Ticketing.SeatHoldAggregate;

public enum HoldStatus
{
    Active,
    Hold,
    Expired
}

public class SeatHold : AggregateRoot<Guid>
{
    private const int DefaultHoldDurationMinutes = 5;

    protected SeatHold()
    {
    } // For EF Core

    public SeatHold(Guid scheduledMovieShowId, CustomerId customerId, SeatPosition seatPosition, DateOnly screeningDate)
    {
        Id = Guid.CreateVersion7();
        ScheduledMovieShowId = scheduledMovieShowId;
        CustomerId = customerId;
        SeatPosition = seatPosition;
        Status = HoldStatus.Active;
        ScreeningDate = screeningDate;

        //AddDomainEvent(new SeatHoldStarted(ScheduledMovieShowId, CustomerId, screeningDate, SeatPosition));
    }

    public CustomerId CustomerId { get; }

    public DateOnly ScreeningDate { get; }

    public Guid ScheduledMovieShowId { get; }
    public SeatPosition SeatPosition { get; }

    public DateTime? ExpiresAt { get; private set; }

    public HoldStatus Status { get; private set; }


    public void ConfirmHold()
    {
        Status = HoldStatus.Hold;
        ExpiresAt = DateTime.UtcNow.Add(TimeSpan.FromMinutes(DefaultHoldDurationMinutes));
        //AddDomainEvent(new SeatHoldConfirmed(ScheduledMovieShowId, CustomerId, ScreeningDate, SeatPosition));
    }

    public void ExtendHold(TimeSpan additionalTime)
    {
        //if (IsExpired())
        //    throw new BusinessException(ErrorCodes.SeatHoldExpired);

        ExpiresAt = ExpiresAt?.Add(additionalTime);
    }

    public bool IsExpired()
    {
        return DateTime.UtcNow > ExpiresAt;
    }

    public bool CanBeConvertedToReservationOrPurchase()
    {
        return !IsExpired();
    }

    public bool IsHold()
    {
        return Status == HoldStatus.Hold && !IsExpired();
    }
}