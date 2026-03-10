using DomainDrivenDesignExample.API.BoundedContexts.Catalog;
using DomainDrivenDesignExample.API.BoundedContexts.Scheduling;
using DomainDrivenDesignExample.API.BoundedContexts.Ticketing.Aggregate;
using DomainDrivenDesignExample.API.BoundedContexts.Ticketing.SeatHoldAggregate;
using DomainDrivenDesignExample.API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

// ReSharper disable All

namespace DomainDrivenDesignExample.API.Infrastructure.Repositories
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<TicketIssuance> TicketIssuances { get; set; } = null!;

        public DbSet<SeatHold> SeatHolds { get; set; } = null!;

        public DbSet<Schedule> Schedules { get; set; } = null!;

        public DbSet<Cinema> Cinemas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersistenceAssembly).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}