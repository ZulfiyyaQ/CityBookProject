using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityBookMVCOnionPersistence.Contexts.Migrations
{
    public partial class UpdateReservationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlaceId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_PlaceId",
                table: "Reservations",
                column: "PlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Places_PlaceId",
                table: "Reservations",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Places_PlaceId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_PlaceId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "Reservations");
        }
    }
}
