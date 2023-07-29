using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaSystem.Data.Migrations
{
    public partial class isActiveField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Tickets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Showtimes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Movies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Genres",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Cinemas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 1,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 2,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 3,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172"),
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7"),
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 1,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 2,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 3,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 4,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 5,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 6,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 7,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 8,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 9,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 10,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 11,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 12,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 13,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 14,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 15,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 16,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 17,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 18,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 19,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 20,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 21,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 22,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 23,
                column: "isActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 24,
                column: "isActive",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Showtimes");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Cinemas");
        }
    }
}
