using DomainDrivenDesignExample.API.BoundedContexts.Catalog;
using DomainDrivenDesignExample.API.BoundedContexts.Catalog.SupplierCustomerContextMap;
using DomainDrivenDesignExample.API.BoundedContexts.Scheduling.Services.SupplierCustomerContextMap;
using DomainDrivenDesignExample.API.BoundedContexts.Ticketing.SeatHoldAggregate;
using DomainDrivenDesignExample.API.BoundedContexts.Ticketing.TicketingAggregate;
using DomainDrivenDesignExample.API.Endpoints.Ticketing;
using DomainDrivenDesignExample.API.Infrastructure.Identities;
using DomainDrivenDesignExample.API.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence(builder.Configuration).AddIdentity(builder.Configuration);


builder.Services.AddScoped<IScheduleQueryService, ScheduleQueryService>();
builder.Services.AddScoped<ICatalogQueryService, CatalogQueryService>();

builder.Services.AddScoped<IScheduleQueryService, ScheduleQueryService>();


builder.Services.AddScoped<ITicketIssuanceApplicationService, TicketIssuanceApplicationService>();
builder.Services.AddScoped<ISeatHoldApplicationService, SeatHoldApplicationService>();

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();


// Seed database on startup
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    await seeder.SeedAsync();
}

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseHttpsRedirection();

//Minimal APIs

app.AddTicketingGroupEndpointExt();
app.Run();