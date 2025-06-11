using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetroLayag.Migrations
{
    /// <inheritdoc />
    public partial class AddStartingStation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StartingStation",
                table: "Passengers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartingStation",
                table: "Passengers");
        }
    }
}
