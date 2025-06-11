using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetroLayag.Migrations
{
    /// <inheritdoc />
    public partial class AddDestinationToPassenger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContactNumber",
                table: "Passengers",
                newName: "Destination");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Destination",
                table: "Passengers",
                newName: "ContactNumber");
        }
    }
}
