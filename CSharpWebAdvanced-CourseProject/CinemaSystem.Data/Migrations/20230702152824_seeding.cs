using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaSystem.Data.Migrations
{
    public partial class seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Address", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, "Varna Dubrovnik 8", "/cinemaImages/cinemaniaVarna.jpg", "Cinemania Varna" },
                    { 2, "Sofia 33", "/cinemaImages/cinemaniaSofia.jpg", "Cinemania Sofia" },
                    { 3, "Plovdiv 15", "/cinemaImages/cinemaniaPlovdiv.jpg", "Cinemania Plovid" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Biography" },
                    { 2, "Drama" },
                    { 3, "Action" },
                    { 4, "Adventure" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "PosterImageUrl", "ReleaseYear", "Title" },
                values: new object[,]
                {
                    { new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172"), "Archaeologist Indiana Jones races against time to retrieve a legendary artifact that can change the course of history.", "https://m.media-amazon.com/images/M/MV5BNDJhODYxYzItOGIwZC00ZTBiLTlmN2MtMjM2MzQyZDVkMGM4XkEyXkFqcGdeQXVyMTUzMDA3Mjc2._V1_SX300.jpg", 2023, "Indiana Jones and the Dial of Destiny" },
                    { new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7"), "As Harvard student Mark Zuckerberg creates the social networking site that would become known as Facebook, he is sued by the twins who claimed he stole their idea and by the co-founder who was later squeezed out of the business.", "https://m.media-amazon.com/images/M/MV5BOGUyZDUxZjEtMmIzMC00MzlmLTg4MGItZWJmMzBhZjE0Mjc1XkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_SX300.jpg", 2010, "The Social Network" }
                });

            migrationBuilder.InsertData(
                table: "MovieGenre",
                columns: new[] { "GenreId", "MovieId" },
                values: new object[,]
                {
                    { 3, new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172") },
                    { 4, new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172") },
                    { 1, new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7") },
                    { 2, new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7") }
                });

            migrationBuilder.InsertData(
                table: "Showtimes",
                columns: new[] { "Id", "CinemaId", "MovieId", "StartTime", "TicketPrice" },
                values: new object[,]
                {
                    { 1, 1, new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7"), new DateTime(2023, 9, 2, 12, 30, 0, 0, DateTimeKind.Unspecified), 15m },
                    { 2, 1, new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7"), new DateTime(2023, 9, 2, 18, 30, 0, 0, DateTimeKind.Unspecified), 15m },
                    { 3, 1, new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7"), new DateTime(2023, 9, 3, 12, 30, 0, 0, DateTimeKind.Unspecified), 12m },
                    { 4, 1, new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7"), new DateTime(2023, 9, 3, 18, 30, 0, 0, DateTimeKind.Unspecified), 12m },
                    { 5, 2, new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7"), new DateTime(2023, 9, 2, 12, 30, 0, 0, DateTimeKind.Unspecified), 15m },
                    { 6, 2, new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7"), new DateTime(2023, 9, 2, 18, 30, 0, 0, DateTimeKind.Unspecified), 15m },
                    { 7, 2, new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7"), new DateTime(2023, 9, 3, 12, 30, 0, 0, DateTimeKind.Unspecified), 12m },
                    { 8, 2, new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7"), new DateTime(2023, 9, 3, 18, 30, 0, 0, DateTimeKind.Unspecified), 12m },
                    { 9, 3, new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7"), new DateTime(2023, 9, 2, 12, 30, 0, 0, DateTimeKind.Unspecified), 15m },
                    { 10, 3, new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7"), new DateTime(2023, 9, 2, 18, 30, 0, 0, DateTimeKind.Unspecified), 15m },
                    { 11, 3, new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7"), new DateTime(2023, 9, 3, 12, 30, 0, 0, DateTimeKind.Unspecified), 12m },
                    { 12, 3, new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7"), new DateTime(2023, 9, 3, 18, 30, 0, 0, DateTimeKind.Unspecified), 12m },
                    { 13, 1, new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172"), new DateTime(2023, 9, 2, 12, 30, 0, 0, DateTimeKind.Unspecified), 15m },
                    { 14, 1, new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172"), new DateTime(2023, 9, 2, 18, 30, 0, 0, DateTimeKind.Unspecified), 15m },
                    { 15, 1, new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172"), new DateTime(2023, 9, 3, 12, 30, 0, 0, DateTimeKind.Unspecified), 12m },
                    { 16, 1, new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172"), new DateTime(2023, 9, 3, 18, 30, 0, 0, DateTimeKind.Unspecified), 12m },
                    { 17, 2, new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172"), new DateTime(2023, 9, 2, 12, 30, 0, 0, DateTimeKind.Unspecified), 15m },
                    { 18, 2, new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172"), new DateTime(2023, 9, 2, 18, 30, 0, 0, DateTimeKind.Unspecified), 15m },
                    { 19, 2, new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172"), new DateTime(2023, 9, 3, 12, 30, 0, 0, DateTimeKind.Unspecified), 12m },
                    { 20, 2, new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172"), new DateTime(2023, 9, 3, 18, 30, 0, 0, DateTimeKind.Unspecified), 12m },
                    { 21, 3, new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172"), new DateTime(2023, 9, 2, 12, 30, 0, 0, DateTimeKind.Unspecified), 15m },
                    { 22, 3, new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172"), new DateTime(2023, 9, 2, 18, 30, 0, 0, DateTimeKind.Unspecified), 15m },
                    { 23, 3, new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172"), new DateTime(2023, 9, 3, 12, 30, 0, 0, DateTimeKind.Unspecified), 12m },
                    { 24, 3, new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172"), new DateTime(2023, 9, 3, 18, 30, 0, 0, DateTimeKind.Unspecified), 12m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MovieGenre",
                keyColumns: new[] { "GenreId", "MovieId" },
                keyValues: new object[] { 3, new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172") });

            migrationBuilder.DeleteData(
                table: "MovieGenre",
                keyColumns: new[] { "GenreId", "MovieId" },
                keyValues: new object[] { 4, new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172") });

            migrationBuilder.DeleteData(
                table: "MovieGenre",
                keyColumns: new[] { "GenreId", "MovieId" },
                keyValues: new object[] { 1, new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7") });

            migrationBuilder.DeleteData(
                table: "MovieGenre",
                keyColumns: new[] { "GenreId", "MovieId" },
                keyValues: new object[] { 2, new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7") });

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Showtimes",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("a622d82d-aed0-44d9-9f4c-577418ca1172"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("ab758330-8d53-4c59-b77c-bca379c1d8b7"));
        }
    }
}
