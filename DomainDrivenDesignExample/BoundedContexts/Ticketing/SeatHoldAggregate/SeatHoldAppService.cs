#region

using DomainDrivenDesignExample.API.SharedKernels;
using DomainDrivenDesignExample.API.SharedKernels.Repositories;
using DomainDrivenDesignExample.API.SharedKernels.ValueObjects;

#endregion

namespace DomainDrivenDesignExample.API.BoundedContexts.Ticketing.SeatHoldAggregate;

public class SeatHoldApplicationService(
    IUnitOfWork unitOfWork,
    IUserContext userContext,
    ISeatHoldRepository seatHoldRepository)
    : ISeatHoldApplicationService
{
    public async Task<AppResult> CreateAsync(CreateSeatHoldRequest request)
    {
        Guid customerId = userContext.UserId;


        //TODO: redis lock can add here for concurrency handling
        List<SeatHold> seatHold =
            (await seatHoldRepository.WhereAsync(x =>
                x.ScheduledMovieShowId == request.ScheduledMovieShowId && x.ScreeningDate == request.ScreeningDate &&
                x.Status == HoldStatus.Hold && x.ExpiresAt > DateTime.UtcNow)).ToList();


        List<SeatPositionDto> hasSeatPositionList = request.SeatPositions.Where(seat =>
            seatHold.Any(x => x.SeatPosition.Equals(new SeatPosition(seat.Row, seat.Number)))).ToList();


        if (hasSeatPositionList.Any())
        {
            SeatPositionDto seat = hasSeatPositionList.First();
            //return appDependencyService.LocalizeError.Error(ErrorCodes.SeatAlreadyHeld, [seat.Row, seat.Number]);
        }


        IEnumerable<SeatHold> customerSeatHolds = await seatHoldRepository.WhereAsync(x =>
            x.CustomerId == customerId && x.ScreeningDate.Equals(request.ScreeningDate) &&
            x.ScheduledMovieShowId == request.ScheduledMovieShowId);


        //idempotency check

        List<SeatPositionDto> newSeats = request.SeatPositions.ToList();


        foreach (SeatPositionDto? seat in request.SeatPositions.Where(seat =>
                     customerSeatHolds.Any(x => x.SeatPosition.Equals(new SeatPosition(seat.Row, seat.Number)))))
            newSeats.Remove(seat);


        foreach (SeatHold? newSeatHold in newSeats.Select(seat =>
                     new SeatHold(request.ScheduledMovieShowId, customerId, new SeatPosition(seat.Row, seat.Number),
                         request.ScreeningDate)))
            await seatHoldRepository.AddAsync(newSeatHold);


        await unitOfWork.CommitAsync();

        return AppResult.SuccessAsNoContent();
    }


    public async Task<AppResult> ConfirmAsync(ConfirmSeatHoldRequest request)
    {
        Guid customerId = userContext.UserId;


        List<SeatHold> seatHolds = (await seatHoldRepository.WhereAsync(x =>
                x.ScheduledMovieShowId == request.ScheduledMovieShowId && x.ScreeningDate == request.ScreeningDate &&
                x.CustomerId == customerId))
            .ToList();


        foreach (SeatHold? seatHold in seatHolds)
        {
            seatHold.ConfirmHold();
            await seatHoldRepository.UpdateAsync(seatHold);
        }

        await unitOfWork.CommitAsync();
        return AppResult.SuccessAsNoContent();
    }
}