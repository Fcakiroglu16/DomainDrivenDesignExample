using DomainDrivenDesignExample.API.BoundedContexts.Catalog;
using DomainDrivenDesignExample.API.BoundedContexts.Scheduling;
using DomainDrivenDesignExample.API.BoundexContexts.ValueObjects;
using DomainDrivenDesignExample.API.Infrastructure.Repositories;
using DomainDrivenDesignExample.API.SharedKernels;
using DomainDrivenDesignExample.API.SharedKernels.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesignExample.API.Infrastructure.Persistence;

public class DataSeeder(AppDbContext db)
{
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        await SeedCinemaAsync(cancellationToken);
        await SeedMoviesAsync(cancellationToken);
        await SeedSchedulesAsync(cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
    }

    private async Task SeedCinemaAsync(CancellationToken cancellationToken)
    {
        if (await db.Cinemas.AnyAsync(cancellationToken))
            return;

        var address = new Address(
            country: "Türkiye",
            city: "İstanbul",
            district: "Kadıköy",
            street: "Bağdat Caddesi No:42",
            postalCode: "34710",
            description: "Metro çıkışına 2 dakika yürüyüş mesafesinde");

        var cinema = new Cinema("CinePlex Kadıköy", address);

        var hall = new CinemaHall("Salon 1", ScreeningTechnology.Standard | ScreeningTechnology.ThreeD);

        // 3 rows (A, B, C) × 10 seats = 30 seats
        foreach (var row in new[] { "A", "B", "C" })
        {
            for (var number = 1; number <= 10; number++)
            {
                var seatType = row switch
                {
                    "A" => number is >= 4 and <= 7 ? SeatType.VIP : SeatType.Regular,
                    "C" => SeatType.Accessible,
                    _ => SeatType.Regular
                };

                hall.AddSeat(new Seat(new SeatPosition(row, number), seatType));
            }
        }

        cinema.AddHall(hall);
        await db.Cinemas.AddAsync(cinema, cancellationToken);

        await db.SaveChangesAsync(cancellationToken);
    }

    private async Task SeedMoviesAsync(CancellationToken cancellationToken)
    {
        if (await db.Movies.AnyAsync(cancellationToken))
            return;

        var movie = new Movie(
            title: "Inception",
            duration: new Duration(148),
            posterImageUrl: "https://example.com/posters/inception.jpg",
            supportedTechnology: ScreeningTechnology.Standard | ScreeningTechnology.ThreeD);

        movie.SetDescription("Rüya içinde rüya: bir hırsız, kurbanının bilinçaltına fikir eker.");
        movie.StartShowing(DateTime.UtcNow);

        await db.Movies.AddAsync(movie, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
    }

    private async Task SeedSchedulesAsync(CancellationToken cancellationToken)
    {
        if (await db.Schedules.AnyAsync(cancellationToken))
            return;

        var movie = await db.Movies.FirstOrDefaultAsync(cancellationToken);
        var hall = await db.Cinemas
            .Include(c => c.Halls)
            .SelectMany(c => c.Halls)
            .FirstOrDefaultAsync(cancellationToken);

        if (movie is null || hall is null)
            return;

        var schedules = new Schedule[]
        {
            new(movie.Id, hall.Id,
                ShowTime.Create(new TimeOnly(10, 0), movie.Duration),
                new Price(150, "TRY")),

            new(movie.Id, hall.Id,
                ShowTime.Create(new TimeOnly(14, 0), movie.Duration),
                new Price(175, "TRY")),

            new(movie.Id, hall.Id,
                ShowTime.Create(new TimeOnly(19, 30), movie.Duration),
                new Price(200, "TRY")),
        };

        await db.Schedules.AddRangeAsync(schedules, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
    }
}
