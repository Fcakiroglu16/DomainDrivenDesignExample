using CinemaTicketingSystem.Domain.BoundedContexts.Ticketing.ValueObjects;
using DomainDrivenDesignExample.API.BoundedContexts.Ticketing.Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainDrivenDesignExample.API.Infrastructure.Persistence.EntityConfigurations;

public class TicketIssuanceEntityConfiguration : IEntityTypeConfiguration<TicketIssuance>
{
    public void Configure(EntityTypeBuilder<TicketIssuance> builder)
    {
        builder.ToTable("TicketIssuance", "Ticketing");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        // Configure properties
        builder.Property(x => x.CustomerId).IsRequired();
        builder.Property(x => x.ScheduledMovieShowId).IsRequired();
        builder.Property(x => x.ScreeningDate).IsRequired();

        builder.Property(x => x.CustomerId)
            .HasConversion(
                customerId => customerId.Value,
                value => new CustomerId(value)
            );

        builder.Metadata.FindNavigation(nameof(TicketIssuance.TicketList))!.SetField("_ticketList");

        builder.HasMany(x => x.TicketList).WithOne(y => y.TicketIssuance);
    }
}