using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityBookMVCOnionPersistence.Contexts.Migrations
{
    public partial class UpdateReservationTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Reservations");
        }
    }
}
