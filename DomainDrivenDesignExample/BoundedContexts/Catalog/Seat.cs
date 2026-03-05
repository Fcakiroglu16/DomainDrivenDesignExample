#region

using CinemaTicketingSystem.Domain.BoundedContexts.Catalog;
using DomainDrivenDesignExample.API.SharedKernels;
using DomainDrivenDesignExample.API.SharedKernels.ValueObjects;

#endregion

namespace DomainDrivenDesignExample.API.BoundedContexts.Catalog;

public class Seat : BaseEntity<Guid>
{
    protected Seat()
    {
    }

    // Constructor
    public Seat(SeatPosition seatPosition, SeatType type)
    {
        SeatPosition = seatPosition;
        Type = type;
        Id = Guid.CreateVersion7();
    }

    public SeatPosition SeatPosition { get; private set; }
    public SeatType Type { get; private set; }
    public bool IsAvailable { get; private set; } = true;

    public virtual CinemaHall CinemaHall { get; set; } = null!;


    // Business behavior methods
    public void ChangeSeatType(SeatType newType)
    {
        Type = newType;
    }

    public void SetAvailability(bool isAvailable)
    {
        IsAvailable = isAvailable;
    }


    public bool IsAccessible()
    {
        return Type == SeatType.Accessible;
    }

    public bool IsVIP()
    {
        return Type == SeatType.VIP;
    }

    public bool IsRegular()
    {
        return Type == SeatType.Regular;
    }
}