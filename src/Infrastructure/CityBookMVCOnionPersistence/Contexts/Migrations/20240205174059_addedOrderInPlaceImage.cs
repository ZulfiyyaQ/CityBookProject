using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityBookMVCOnionPersistence.Contexts.Migrations
{
    public partial class addedOrderInPlaceImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "PlaceImage",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "PlaceImage");
        }
    }
}
