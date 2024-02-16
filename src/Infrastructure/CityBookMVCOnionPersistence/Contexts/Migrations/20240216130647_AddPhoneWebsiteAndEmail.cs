using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityBookMVCOnionPersistence.Contexts.Migrations
{
    public partial class AddPhoneWebsiteAndEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Places",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Places",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Places",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "Places");
        }
    }
}
