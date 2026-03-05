using CinemaTicketingSystem.Application.Catalog.Services;
using DomainDrivenDesignExample.API.BoundedContexts.Catalog.SupplierCustomerContextMap;
using DomainDrivenDesignExample.API.BoundedContexts.Scheduling.Services.SupplierCustomerContextMap;
using DomainDrivenDesignExample.API.BoundedContexts.Ticketing.Aggregate;
using DomainDrivenDesignExample.API.BoundedContexts.Ticketing.SeatHoldAggregate;
using DomainDrivenDesignExample.API.BoundexContexts.Ticketing;
using DomainDrivenDesignExample.API.Endpoints.Ticketing.TicketIssuance.Create;
using DomainDrivenDesignExample.API.SharedKernels;
using DomainDrivenDesignExample.API.SharedKernels.ValueObjects;

namespace DomainDrivenDesignExample.API.BoundedContexts.Ticketing.TicketingAggregate;

public record CreateTicketIssuanceResponse(Guid CreatedTicketIssuanceId);

public class TicketIssuanceApplicationService(
    ITicketIssuanceRepository ticketIssuanceRepository,
    IUnitOfWork unitOfWork,
    ISeatHoldRepository seatHoldRepository,
    IUserContext userContext,
    IScheduleQueryService scheduleQueryService,
    ICatalogQueryService catalogQueryService) : ITicketIssuanceApplicationService
{
    public async Task<AppResult<CreateTicketIssuanceResponse>> Create(CreateTicketIssuanceRequest request)
    {
        var customerId = userContext.UserId;

        var scheduleInfo = await scheduleQueryService.GetScheduleInfo(request.ScheduledMovieShowId);

        if (scheduleInfo.IsFail)
            return AppResult<CreateTicketIssuanceResponse>.Error(scheduleInfo.ProblemDetails!);


        AppResult<GetCatalogInfoResponse> catalogInfo =
            await catalogQueryService.GetCinemaInfo(scheduleInfo.Data!.CinemaHallId, scheduleInfo.Data.MovieId);

        if (catalogInfo.IsFail) return AppResult<CreateTicketIssuanceResponse>.Error(catalogInfo.ProblemDetails!);


        List<SeatHold> userSeatHoldList = (await seatHoldRepository.WhereAsync(x =>
                x.CustomerId == customerId &&
                x.ScheduledMovieShowId == request.ScheduledMovieShowId && x.ScreeningDate == request.ScreeningDate))
            .ToList();


        //if (!userSeatHoldList.Any())
        //    return appDependencyService.LocalizeError.Error<CreateTicketIssuanceResponse>(ErrorCodes.NoSeatHoldFound);


        //if (userSeatHoldList.Any(seatHold => seatHold.IsExpired()))
        //    return appDependencyService.LocalizeError.Error<CreateTicketIssuanceResponse>(
        //        ErrorCodes.SeatHoldExpired);


        //    // Fetch confirmed seats from tickets
        List<SeatPosition> confirmedTicketSeatPositions =
            (await ticketIssuanceRepository.GetConfirmedTicketsIssuanceByScheduleIdAndScreeningDate(
                request.ScheduledMovieShowId,
                request.ScreeningDate)).SelectMany(x => x.TicketList)
            .Select(x => x.SeatPosition)
            .ToList();

        //    // Fetch confirmed seats from holds
        List<SeatPosition> confirmedSeatHoldSeatPositions =
            (await seatHoldRepository.GetConfirmedListByScheduleIdAndScreeningDate(request.ScheduledMovieShowId,
                request.ScreeningDate)).Where(x => x.CustomerId != customerId)
            .Select(x => x.SeatPosition)
            .ToList();


        //    // Merge uniquely by seat coordinates
        List<SeatPosition> occupiedSeatPositions = confirmedTicketSeatPositions
            .Concat(confirmedSeatHoldSeatPositions)
            .DistinctBy(sp => (sp.Row, sp.Number))
            .ToList();


        //foreach (SeatHold? seat in userSeatHoldList)
        //{
        //    bool seatTaken = occupiedSeatPositions.Any(x =>
        //        x.Row == seat.SeatPosition.Row && x.Number == seat.SeatPosition.Number);
        //    if (seatTaken)
        //        return appDependencyService.LocalizeError.Error<CreateTicketIssuanceResponse>(ErrorCodes.DuplicateSeat,
        //            [seat.SeatPosition.Row, seat.SeatPosition.Number]);
        //}


        TicketIssuance newTicketIssuance =
            new TicketIssuance(request.ScheduledMovieShowId, customerId, request.ScreeningDate);

        foreach (SeatHold? seat in userSeatHoldList)
            newTicketIssuance.AddTicket(seat.SeatPosition, scheduleInfo.Data.TicketPrice);

        await ticketIssuanceRepository.AddAsync(newTicketIssuance);

        await unitOfWork.CommitAsync();

        return AppResult<CreateTicketIssuanceResponse>.SuccessAsCreated(
            new CreateTicketIssuanceResponse(newTicketIssuance.Id), string.Empty);
    }
}