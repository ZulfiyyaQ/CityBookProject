using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityBookMVCOnionPersistence.Contexts.Migrations
{
    public partial class AddedColumnsToPlace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DayOrMonth",
                table: "Places",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Friday",
                table: "Places",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "FridayTo",
                table: "Places",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Monday",
                table: "Places",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "MondayTo",
                table: "Places",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Saturday",
                table: "Places",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "SaturdayTo",
                table: "Places",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Sunday",
                table: "Places",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "SundayTo",
                table: "Places",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Thursday",
                table: "Places",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ThursdayTo",
                table: "Places",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Tuesday",
                table: "Places",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TuesdayTo",
                table: "Places",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Wednesday",
                table: "Places",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "WednesdayTo",
                table: "Places",
                type: "time",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOrMonth",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Friday",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "FridayTo",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Monday",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "MondayTo",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Saturday",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "SaturdayTo",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Sunday",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "SundayTo",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Thursday",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "ThursdayTo",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Tuesday",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "TuesdayTo",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Wednesday",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "WednesdayTo",
                table: "Places");
        }
    }
}
