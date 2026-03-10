using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainDrivenDesignExample.API.Migrations
{
    /// <inheritdoc />
    public partial class check2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                schema: "Ticketing",
                table: "Tickets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                schema: "Ticketing",
                table: "Tickets",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                schema: "Ticketing",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Currency",
                schema: "Ticketing",
                table: "Tickets");
        }
    }
}
