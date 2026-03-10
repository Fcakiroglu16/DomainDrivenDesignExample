using DomainDrivenDesignExample.API.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenDesignExample.API.Infrastructure.Persistence;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(dbOptionBuilder =>
        {
            dbOptionBuilder.UseSqlServer(configuration.GetConnectionString("SqlServer"));
        });

        return services;
    }
}