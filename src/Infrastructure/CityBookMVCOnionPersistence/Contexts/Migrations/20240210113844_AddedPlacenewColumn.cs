using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityBookMVCOnionPersistence.Contexts.Migrations
{
    public partial class AddedPlacenewColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeReviews_AspNetUsers_UserId",
                table: "EmployeeReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Places_AspNetUsers_UserId1",
                table: "Places");

            migrationBuilder.DropIndex(
                name: "IX_Places_UserId1",
                table: "Places");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeReviews_UserId",
                table: "EmployeeReviews");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "EmployeeReviews");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Places",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Places_UserId",
                table: "Places",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Places_AspNetUsers_UserId",
                table: "Places",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Places_AspNetUsers_UserId",
                table: "Places");

            migrationBuilder.DropIndex(
                name: "IX_Places_UserId",
                table: "Places");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Places",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Places",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "EmployeeReviews",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Places_UserId1",
                table: "Places",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeReviews_UserId",
                table: "EmployeeReviews",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeReviews_AspNetUsers_UserId",
                table: "EmployeeReviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Places_AspNetUsers_UserId1",
                table: "Places",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
