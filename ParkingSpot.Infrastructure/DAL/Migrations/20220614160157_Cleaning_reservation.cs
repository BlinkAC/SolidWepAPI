using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingSpot.Infrastructure.DAL.Migrations
{
    public partial class Cleaning_reservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "reservations",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "reservations");
        }
    }
}
