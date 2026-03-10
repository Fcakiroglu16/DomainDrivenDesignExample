using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainDrivenDesignExample.API.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "catalogs");

            migrationBuilder.EnsureSchema(
                name: "scheduling");

            migrationBuilder.EnsureSchema(
                name: "Ticketing");

            migrationBuilder.CreateTable(
                name: "Cinemas",
                schema: "catalogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    District = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AddressDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cinemas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                schema: "catalogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OriginalTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SupportedTechnology = table.Column<int>(type: "int", nullable: false),
                    PosterImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    DurationMinutes = table.Column<double>(type: "float", nullable: false),
                    EarliestShowingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShowingStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShowingEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsCurrentlyShowing = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                schema: "scheduling",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HallId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    ShowTime_StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    ShowTime_EndTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SeatHolds",
                schema: "Ticketing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScreeningDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ScheduledMovieShowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Row = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatHolds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketIssuance",
                schema: "Ticketing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduledMovieShowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScreeningDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IsDiscountApplied = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketIssuance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CinemaHalls",
                schema: "catalogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SupportedTechnologies = table.Column<int>(type: "int", nullable: false),
                    IsOperational = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CinemaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CinemaHalls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CinemaHalls_Cinemas_CinemaId",
                        column: x => x.CinemaId,
                        principalSchema: "catalogs",
                        principalTable: "Cinemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                schema: "Ticketing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Row = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    TicketCode = table.Column<string>(type: "nchar(6)", fixedLength: true, maxLength: 6, nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    UsedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TicketIssuanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_TicketIssuance_TicketIssuanceId",
                        column: x => x.TicketIssuanceId,
                        principalSchema: "Ticketing",
                        principalTable: "TicketIssuance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                schema: "catalogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Row = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CinemaHallId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_CinemaHalls_CinemaHallId",
                        column: x => x.CinemaHallId,
                        principalSchema: "catalogs",
                        principalTable: "CinemaHalls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CinemaHalls_CinemaId",
                schema: "catalogs",
                table: "CinemaHalls",
                column: "CinemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_CinemaHallId",
                schema: "catalogs",
                table: "Seats",
                column: "CinemaHallId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketIssuanceId",
                schema: "Ticketing",
                table: "Tickets",
                column: "TicketIssuanceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies",
                schema: "catalogs");

            migrationBuilder.DropTable(
                name: "Schedules",
                schema: "scheduling");

            migrationBuilder.DropTable(
                name: "SeatHolds",
                schema: "Ticketing");

            migrationBuilder.DropTable(
                name: "Seats",
                schema: "catalogs");

            migrationBuilder.DropTable(
                name: "Tickets",
                schema: "Ticketing");

            migrationBuilder.DropTable(
                name: "CinemaHalls",
                schema: "catalogs");

            migrationBuilder.DropTable(
                name: "TicketIssuance",
                schema: "Ticketing");

            migrationBuilder.DropTable(
                name: "Cinemas",
                schema: "catalogs");
        }
    }
}
