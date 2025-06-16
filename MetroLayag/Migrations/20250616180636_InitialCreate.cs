using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MetroLayag.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    StartingStation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCanceled = table.Column<bool>(type: "bit", nullable: false),
                    HasDisembarked = table.Column<bool>(type: "bit", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Station = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "Role", "Station", "Username" },
                values: new object[,]
                {
                    { 1, "$2a$11$fJ8yUYDZQsMZVc/j8JNV.e7ijmTc0S3IAYtbnmvfRMyEvN6VvFHBu", "MainAdmin", "Main Office", "admin" },
                    { 2, "$2a$11$fJ8yUYDZQsMZVc/j8JNV.e7ijmTc0S3IAYtbnmvfRMyEvN6VvFHBu", "StationAdmin", "Escolta", "escolta" },
                    { 3, "$2a$11$fJ8yUYDZQsMZVc/j8JNV.e7ijmTc0S3IAYtbnmvfRMyEvN6VvFHBu", "StationAdmin", "Lawton", "lawton" },
                    { 4, "$2a$11$fJ8yUYDZQsMZVc/j8JNV.e7ijmTc0S3IAYtbnmvfRMyEvN6VvFHBu", "StationAdmin", "Quinta", "quinta" },
                    { 5, "$2a$11$fJ8yUYDZQsMZVc/j8JNV.e7ijmTc0S3IAYtbnmvfRMyEvN6VvFHBu", "StationAdmin", "PUP", "pup" },
                    { 6, "$2a$11$fJ8yUYDZQsMZVc/j8JNV.e7ijmTc0S3IAYtbnmvfRMyEvN6VvFHBu", "StationAdmin", "Sta. Ana", "staana" },
                    { 7, "$2a$11$fJ8yUYDZQsMZVc/j8JNV.e7ijmTc0S3IAYtbnmvfRMyEvN6VvFHBu", "StationAdmin", "Lambingan", "lambingan" },
                    { 8, "$2a$11$fJ8yUYDZQsMZVc/j8JNV.e7ijmTc0S3IAYtbnmvfRMyEvN6VvFHBu", "StationAdmin", "Valenzuela", "valenzuela" },
                    { 9, "$2a$11$fJ8yUYDZQsMZVc/j8JNV.e7ijmTc0S3IAYtbnmvfRMyEvN6VvFHBu", "StationAdmin", "Hulo", "hulo" },
                    { 10, "$2a$11$fJ8yUYDZQsMZVc/j8JNV.e7ijmTc0S3IAYtbnmvfRMyEvN6VvFHBu", "StationAdmin", "Guadalupe", "guadalupe" },
                    { 11, "$2a$11$fJ8yUYDZQsMZVc/j8JNV.e7ijmTc0S3IAYtbnmvfRMyEvN6VvFHBu", "StationAdmin", "Maybunga", "maybunga" },
                    { 12, "$2a$11$fJ8yUYDZQsMZVc/j8JNV.e7ijmTc0S3IAYtbnmvfRMyEvN6VvFHBu", "StationAdmin", "San Joaquin", "sanjoaquin" },
                    { 13, "$2a$11$fJ8yUYDZQsMZVc/j8JNV.e7ijmTc0S3IAYtbnmvfRMyEvN6VvFHBu", "StationAdmin", "Kalawaan", "kalawaan" },
                    { 14, "$2a$11$fJ8yUYDZQsMZVc/j8JNV.e7ijmTc0S3IAYtbnmvfRMyEvN6VvFHBu", "StationAdmin", "Pinagbuhatan", "pinagbuhatan" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
