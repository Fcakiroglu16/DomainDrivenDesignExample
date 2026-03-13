using DomainDrivenDesignExample.API.BoundedContexts.Catalog.Repositories;
using DomainDrivenDesignExample.API.BoundedContexts.Scheduling;
using DomainDrivenDesignExample.API.BoundedContexts.Scheduling.Services.SupplierCustomerContextMap;
using DomainDrivenDesignExample.API.BoundedContexts.Ticketing.SeatHoldAggregate;
using DomainDrivenDesignExample.API.BoundedContexts.Ticketing.TicketingAggregate;
using DomainDrivenDesignExample.API.Infrastructure.Persistence.Repositories;
using DomainDrivenDesignExample.API.Infrastructure.Repositories;
using DomainDrivenDesignExample.API.SharedKernels.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesignExample.API.Infrastructure.Persistence;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<ISeatHoldRepository, SeatHoldRepository>();

        services.AddScoped<ITicketIssuanceRepository, TicketIssuanceRepository>();

        services.AddScoped<ICinemaRepository, CinemaRepository>();
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IScheduleRepository, ScheduleRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();


        services.AddDbContext<AppDbContext>(dbOptionBuilder =>
        {
            dbOptionBuilder.UseSqlServer(configuration.GetConnectionString("SqlServer"));
        });

        services.AddScoped<DataSeeder>();



        return services;
    }
}