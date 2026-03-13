using System.Net;
using DomainDrivenDesignExample.API.BoundedContexts.Catalog.Repositories;
using DomainDrivenDesignExample.API.SharedKernels;

namespace DomainDrivenDesignExample.API.BoundedContexts.Catalog.SupplierCustomerContextMap;

public class CatalogQueryService(
    ICinemaRepository cinemaRepository,
    IMovieRepository movieRepository,
    ILogger<CatalogQueryService> logger) : ICatalogQueryService
{
    public async Task<AppResult<GetCatalogInfoResponse>> GetCinemaInfo(Guid hallId, Guid movieId)
    {
        Catalog.Movie? movie = await movieRepository.GetByIdAsync(movieId);

        if (movie is null)
        {
            logger.LogWarning($"Movie with Id: {movieId} not found.");
            //return appDependencyService.LocalizeError.Error<GetCatalogInfoResponse>(ErrorCodes.MovieNotFound,
            //    HttpStatusCode.NotFound);
        }

        Catalog.Cinema? cinema = await cinemaRepository.GetByHallId(hallId);

        if (cinema is null)
        {
            logger.LogWarning($"Cinema with Hall Id: {hallId} not found.");
            //return appDependencyService.LocalizeError.Error<GetCatalogInfoResponse>(ErrorCodes.CinemaNotFound,
            //    HttpStatusCode.NotFound);
        }

        CinemaHall? hall = cinema.Halls.FirstOrDefault(h => h.Id == hallId);

        if (hall is null)
        {
            logger.LogWarning($"Cinema hall with Id: {hallId} not found in cinema with Name: {cinema.Name}.");
            //return appDependencyService.LocalizeError.Error<GetCatalogInfoResponse>(ErrorCodes.CinemaHallNotFound,
            //    HttpStatusCode.NotFound);
        }

        return AppResult<GetCatalogInfoResponse>.SuccessAsOk(new GetCatalogInfoResponse(cinema.Name, hall.Name,
            movie.Title, hall.Capacity));
    }
}